using MatsvinnApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatsvinnApp.FileManagement;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatsvinnApp
{
    public partial class FoodEditPage : ContentPage
    {
        DateTime maxDate;
        DateTime minDate;
        DateTime selectedDate;

        public FoodEditPage(Food foodInfo)
        {
            selectedDate = foodInfo.Date;
            maxDate = selectedDate;
            minDate = selectedDate;
            InitializeComponent();

            datePicker.MaximumDate = maxDate;
            datePicker.MinimumDate = minDate;
            datePicker.Date = selectedDate;

            foodContent.Text = foodInfo.Caption;
            foodName.Text = foodInfo.Name;
        }

        async void SaveButtonClicked(object sender, EventArgs args)
        {
            Food food = new Food(selectedDate, foodName.Text, foodContent.Text);
            FileManager fileManager = new FileManager();
            fileManager.EnterFood(food);
            await Navigation.PushAsync(new AdminCalenderPage { });
        }

        async void DeleteButtonClicked(object sender, EventArgs args)
        {
            Food food = new Food(selectedDate, foodName.Text, foodContent.Text);
            FileManager fileManager = new FileManager();
            fileManager.DeleteFood(food);
            await Navigation.PushAsync(new AdminCalenderPage { });
        }
    }
}