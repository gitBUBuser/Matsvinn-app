using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatsvinnApp
{
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        async void OnTap(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new LoginPage { });
        }
    }
}