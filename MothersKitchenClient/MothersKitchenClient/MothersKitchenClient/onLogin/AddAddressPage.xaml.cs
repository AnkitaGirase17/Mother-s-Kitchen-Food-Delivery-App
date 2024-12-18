using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MothersKitchenClient.onLogin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAddressPage : ContentPage
    {
        public AddAddressPage()
        {
            InitializeComponent();
        }

        public async void InsertDeliveryAddress()
        {
            try
            {
                // Initialize HttpClient
                var client = new HttpClient();

                // Prepare the data to send
                var formData = new Dictionary<string, string>
        {
            { "PIN", PIN_Entry.Text?.Trim() ?? "" },
            { "ADL1", ADL1_Entry.Text?.Trim() ?? "" },
            { "ADL2", ADL2_Entry.Text?.Trim() ?? "" },
            { "ADL3", ADL3_Entry.Text?.Trim() ?? "" },
            { "CITY", City_Entry.Text?.Trim() ?? "" },
            { "STATE", State_Entry.Text?.Trim() ?? "" },
            { "STATUS", "Active" } // Default status
        };

                // Prepare the HTTP request
                var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.223.77/Client/CreateDeliveryAddress")
                {
                    Content = new FormUrlEncodedContent(formData) // Add form data
                };

                // Send the request and get the response
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode(); // Ensure the request was successful

                // Handle the response content
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<DELIVERY_ADDRESSClass>(responseContent);

                // Check if the response status is success
                if (responseJson?.STATUS == "SUCCESS")
                {
                    await DisplayAlert("Success", "Delivery address added successfully.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Failed to add delivery address. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle and log exceptions
                await DisplayAlert("Exception", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void Add_Delivery_Address_Btn(object sender, EventArgs e)
        {
            InsertDeliveryAddress();

        }
    }
}