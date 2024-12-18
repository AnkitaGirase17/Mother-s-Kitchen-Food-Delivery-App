using MothersKitchenClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MothersKitchenClient.onLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
          
            LoadPersonalInfo();
            LoadAddress();
        }

        private async void LoadPersonalInfo()
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/showClientName");
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
            Name_entry.Text = JSON_DATA["CNAME"].ToString();
            mobile_entry.Text = JSON_DATA["MOBILE"].ToString();
        }




        private async void LoadAddress()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/showClientName");
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var JSON_DATA = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
            Cname_entry.Text =JSON_DATA["CNAME"].ToString();
            city_entry.Text = JSON_DATA["CITY"].ToString ();
            state_entry.Text=JSON_DATA["STATE"].ToString();
            pin_entry.Text = JSON_DATA["PIN"].ToString();
            mobile1_entry.Text = JSON_DATA["MOBILE"].ToString();


        }

       
    }
}