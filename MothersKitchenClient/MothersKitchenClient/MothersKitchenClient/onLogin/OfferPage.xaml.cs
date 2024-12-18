using MothersKitchenClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MothersKitchenClient.onLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfferPage : ContentPage
    {
        public OfferPage()
        {
            InitializeComponent();
            GetAllOfferdItems();


        }

        public async void GetAllOfferdItems()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/ShowAllItem");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                
                var responseContent = await response.Content.ReadAsStringAsync();
                var allItems = JsonConvert.DeserializeObject<List<ITEMClass>>(responseContent);

               
                var discountedItems = allItems
                    .Where(item =>
                    {
                        if (decimal.TryParse(item.MRP, out decimal mrp) && decimal.TryParse(item.SP, out decimal sp))
                        {
                            return mrp <= sp * 0.9m;
                        }
                        return false; 
                    })
                    .OrderByDescending(item => decimal.Parse(item.SP) - decimal.Parse(item.MRP)) // Sort by highest discount
                    .ToList();


                OfferItemList.ItemsSource = discountedItems;
            }
            catch (Exception ex)
            {
             
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void OfferItemList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as ITEMClass;
            if (selectedItem != null)
            {
                await Navigation.PushAsync(new ItemDetailsPage(selectedItem.IID.ToString()));
            }
        }
    }
}