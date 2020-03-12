using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatsvinnApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatsvinnApp
{
    public partial class FoodInfo : ContentPage
    {
        Food food;
        public FoodInfo(Food selectedFood)
        {
            InitializeComponent();
            food = selectedFood;
            Title = food.Name;
            FoodInf.Text = food.Caption;
            date.Text = food.Date;

        }
    }
}