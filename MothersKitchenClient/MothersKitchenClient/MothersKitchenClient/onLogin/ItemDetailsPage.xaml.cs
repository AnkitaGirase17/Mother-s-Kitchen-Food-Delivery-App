using MothersKitchenClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MothersKitchenClient.onLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailsPage : ContentPage
    {
        private string itemID;
        public ItemDetailsPage(string IID)
        {
            InitializeComponent();
            itemID = IID;
            LoadItems(IID);
        }

        private int currentQuantity = 1; // Initialize quantity
        private const int maxQuantity = 100; // Set a max limit (if applicable)
        private const int minQuantity = 1; // Set a min limit (1 or 0 depending on your app logic)

        private async void LoadItems(string IID)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/ShowItemDetails");
            var content = new MultipartFormDataContent();


            content.Add(new StringContent(IID), "IID");
            request.Content = content;
            var response = await client.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
            ProductName_Entry.Text = JSON_DATA["INAME"].ToString();
            SP_Entry.Text = JSON_DATA["SP"].ToString();
            MRP_Entry.Text = JSON_DATA["MRP"].ToString();
            Contents_Entry.Text = JSON_DATA["CONTENTS"].ToString();
            Brief_Entry.Text = JSON_DATA["BRIEF"].ToString();
            AI_Entry.Text = JSON_DATA["AI"].ToString();
            ItemsImages_CV.ItemsSource = JSON_DATA["ICON"].ToString();
            currentQuantity = Convert.ToInt32(JSON_DATA["QT"].ToString());
            QuantityLabel.Text = currentQuantity.ToString();





        }

      

        private async Task UpdateQuantityInCart(int quantity, string IID)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.145.77/Client/additemincart");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(IID), "IID");
                content.Add(new StringContent(quantity.ToString()), "QT");
              

                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    // Success message or UI update
                    await DisplayAlert("Success", "Quantity updated in cart.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update quantity in cart.", "OK");
                }
            }
            catch (Exception EE)
            {
                await DisplayAlert("Error", EE.Message, "OK");
            }
        }

        private async void QT_DECREAMENT_BUTTON_Clicked(object sender, EventArgs e)
        {

            if (currentQuantity > minQuantity)
            {
                currentQuantity--;
                QuantityLabel.Text = currentQuantity.ToString();

                // Update quantity in the cart (if item exists)
                await UpdateQuantityInCart(currentQuantity, itemID);
            }
        }

        private async void QT_INCREAMENT_BUTTON_Clicked(object sender, EventArgs e)
        {
            if (currentQuantity < maxQuantity)
            {
                currentQuantity++;
                QuantityLabel.Text = currentQuantity.ToString();

                // Update quantity in the cart (if item exists)
                await UpdateQuantityInCart(currentQuantity, itemID);
            }
        }



        private async Task<bool> AddItemToCart(string IID, int quantity)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/additemincart");
                var content = new MultipartFormDataContent();

                content.Add(new StringContent(IID), "IID");
                content.Add(new StringContent(quantity.ToString()), "QT");

                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception EE)
            {
            
                await DisplayAlert("Error", "An error occurred: " + EE.Message, "OK");
                return false; 
            }
        }




        private async void AddToCart_Button_Clicked(object sender, EventArgs e)
        {
            if (currentQuantity > 0)
            {
             
                var isAddedToCart = await AddItemToCart(itemID, currentQuantity);

                if (isAddedToCart)
                {
                    
                    await DisplayAlert("Success", "Item added to cart successfully!", "OK");

                  
                    await Navigation.PushAsync(new CartPage());
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add item to cart. Please try again.", "OK");
                }
            }
            else
            {
               
                await DisplayAlert("Error", "Cannot add item with zero quantity to the cart.", "OK");
            }

        }
    }
}
