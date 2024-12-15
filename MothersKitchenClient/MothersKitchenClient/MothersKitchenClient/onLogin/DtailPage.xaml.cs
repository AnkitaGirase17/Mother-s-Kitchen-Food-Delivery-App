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
    public partial class DtailPage : ContentPage
    {
        public DtailPage()
        {
            InitializeComponent();
            //LoadItems ();

        }

        private void delete_qt_btn_Clicked(object sender, EventArgs e)
        {
            int currentQuantity = int.Parse(quntityEntry.Text);
            if (currentQuantity > 0)
            {
                currentQuantity--;
                quntityEntry.Text = currentQuantity.ToString();
            }
        }

        private void add_qt_btn_Clicked(object sender, EventArgs e)
        {
            int currentQuantity = int.Parse(quntityEntry.Text);
            currentQuantity++;
            quntityEntry.Text = currentQuantity.ToString();
        }

        private void Add_to_cart_btn_Clicked(object sender, EventArgs e)
        {

        }
    }
}