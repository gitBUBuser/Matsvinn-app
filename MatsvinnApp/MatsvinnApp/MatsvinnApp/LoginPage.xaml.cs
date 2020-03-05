using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatsvinnApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async void LoginButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new StudentMainPage { });
        }
    }
}