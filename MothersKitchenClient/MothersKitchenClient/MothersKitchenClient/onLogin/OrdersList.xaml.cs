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
    public partial class OrdersList : ContentPage
    {
        public OrdersList()
        {
            InitializeComponent();
            AllIetmforCart();
        }
        public async void AllIetmforCart()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/AllIetmforCart");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            var responseContent = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<List<ORDER_SUMMERYClass>>(responseContent);
            OrderListView.ItemsSource = JSON_DATA;

        }

        private async void OrderListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as ITEMClass;
            if (selectedItem != null)
            {
                await Navigation.PushAsync(new ItemDetailsPage(selectedItem.IID.ToString()));
            }
        }
    }
}