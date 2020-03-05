using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatsvinnApp
{
    public partial class StudentMainPage : ContentPage
    {
        public StudentMainPage()
        {
            InitializeComponent();
        }

        async void ButtonClickedFoodOfTheDay(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new FOTDPage { });
        }

        async void ButtonClickedCalender(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new StudentCalenderPage { });
        }
    }
}