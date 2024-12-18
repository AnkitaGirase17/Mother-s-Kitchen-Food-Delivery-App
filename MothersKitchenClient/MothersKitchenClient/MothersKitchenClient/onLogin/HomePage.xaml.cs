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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
           
                string uid = Application.Current.Properties["UID"].ToString();

         
            Application.Current.Properties["UID"] = "7588318518";
            Home_Image.Source = ImageSource.FromResource("MothersKitchenClient.img.home.png");
            Offers_Image.Source = ImageSource.FromResource("MothersKitchenClient.img.offers.png");
            Cart_Image.Source = ImageSource.FromResource("MothersKitchenClient.img.cart.png");
            Order_Image.Source = ImageSource.FromResource("MothersKitchenClient.img.order.png");
            Profile_Image.Source = ImageSource.FromResource("MothersKitchenClient.img.user.png");
            ItemsListView.RefreshCommand = new Command(() =>
            {
                //Do your stuff.    
                //GetAllItems();
                ItemsListView.IsRefreshing = false;
            });
            GetAllCategorties();
            GetAllItems();
        }

        public async void GetAllCategorties()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/ShowAllCategories");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            var responseContent = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<List<CartClass>>(responseContent);
            CategoryCollectionView.ItemsSource = JSON_DATA;

        }

        public async void GetAllItems()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/ShowAllItem");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            var responseContent = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<List<ITEMClass>>(responseContent);
            ItemsListView.ItemsSource = JSON_DATA;

        }



        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["UID"] = "";
            Navigation.PopAsync();
        }

        private void CategoryCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void ItemsListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as ITEMClass;
            if (selectedItem != null)
            {
                await Navigation.PushAsync(new ItemDetailsPage(selectedItem.IID.ToString()));
            }



        }

        private void Home_Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void Order_Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OrdersList());
        }

        private void Offers_Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OfferPage());
        }

        private void Cart_Image_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage());
        }

        private void CartValueLabel_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CartPage());
        }

        private void Profile_Image_Tapped(object sender, EventArgs e)
        {
            Application.Current.Properties["UID"] = "";
            Navigation.PushAsync(new ProfilePage());
        }
    }
}