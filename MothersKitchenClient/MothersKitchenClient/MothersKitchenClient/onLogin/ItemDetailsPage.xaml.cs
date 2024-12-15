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
    public partial class ItemDetailsPage : ContentPage
    {
        public ItemDetailsPage(string IID)
        {
            InitializeComponent();
            LoadItems(IID);
        }


        private async void LoadItems(string IID)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.145.77/Client/ShowItemDetails");
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
            iTEMiMAGE.Source = JSON_DATA["ICON"].ToString();
            img1.Source = JSON_DATA["IMG2"].ToString() ;
            img2.Source = JSON_DATA["IMG1"].ToString();
           

            



        }

        private void QT_DECREAMENT_BUTTON_Clicked(object sender, EventArgs e)
        {

        }

        private void QT_INCREAMENT_BUTTON_Clicked(object sender, EventArgs e)
        {

        }

        private void AddToCart_Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}