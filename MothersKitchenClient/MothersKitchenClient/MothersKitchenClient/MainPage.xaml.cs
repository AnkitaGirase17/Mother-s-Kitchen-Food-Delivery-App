using MothersKitchenClient.onLogin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MothersKitchenClient
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            AppLogo_Image.Source = ImageSource.FromResource("MothersKitchenClient.img.mk.png");

        }

        private void Login_Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Mobile_Entry.Text.Length == 10)
                {
                    MobileStack.IsVisible = false;
                    OTPStack.IsVisible = true;
                }
                else
                {
                    DisplayAlert("Alert", "Invalid Mobile Number", "Ok");
                }
            }
            catch (Exception EE) { }
        }

        private void VerifyOTP_Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(FN.Text) != true && string.IsNullOrEmpty(SN.Text) != true && string.IsNullOrEmpty(TN.Text) != true && string.IsNullOrEmpty(FTN.Text) != true)
            {
                var OTP = FN.Text + SN.Text + TN.Text + FTN.Text;
                if (OTP == "1234")
                {
                    Application.Current.Properties["UID"] = Mobile_Entry.Text;
                    Navigation.PushAsync(new HomePage());
                }
                else
                {
                    DisplayAlert("Alert", "Invalid OTP", "Ok");
                }
            }
            else
            {
                DisplayAlert("Alert", "OTP Cannot Be Null", "Ok");
            }
        }

        private void CancelOTP_Button_Clicked(object sender, EventArgs e)
        {
            MobileStack.IsVisible = true;
            OTPStack.IsVisible = false;
        }
    }
}
