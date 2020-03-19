using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatsvinnApp.Models;
using MatsvinnApp.FileManagement;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Globalization;

namespace MatsvinnApp
{
   
    public partial class AdminCalenderPage : ContentPage
    {
        FileManager fileManager;
        ListView dateInfo;
        ObservableCollection<Food> foods;
        Label dateLabel;

        string[] days =
            {
                "Mån",
                "Tis",
                "Ons",
                "Tor",
                "Fre",
                "Lör",
                "Sön"
            };

        string[] months =
        {
                "Januari",
                "Februari",
                "Mars",
                "April",
                "Maj",
                "Juni",
                "Juli",
                "Augusti",
                "September",
                "Oktober",
                "November",
                "December"
            };

        public AdminCalenderPage()
        {
            DateTime dateSave = new DateTime();
            foods = new ObservableCollection<Food>();

            dateLabel = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 20,
                TextColor = Color.DarkGray

            };

            dateInfo = new ListView()
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                SeparatorColor = Color.LightGray,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label foodNameLabel = new Label();
                    foodNameLabel.SetBinding(Label.TextProperty, "Name");

                    Label captionLabel = new Label()
                    {
                        TextColor = Color.DimGray
                    };
                    captionLabel.SetBinding(Label.TextProperty, "Caption");

                    BoxView boxView = new BoxView()
                    {

                    };

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Spacing = 0,
                            Children =
                                    {
                                        foodNameLabel,
                                        captionLabel
                                    }
                        }

                    };
                })
            };

            Button addButton = new Button()
            {
                Text = "+",
                HeightRequest = 30,
                WidthRequest = 30
            };
            addButton.Clicked += delegate { AddFood(dateSave); };



            StackLayout bottomLayout = new StackLayout()
            {
                Margin = new Thickness(30, 30, 30, 0),
                Spacing = 20,
                Children =
                {
                    dateLabel,
                    addButton,
                    dateInfo
                }
            };

            dateInfo.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var tapped = e.SelectedItem;
                Navigation.PushAsync(new FoodEditPage((Food)tapped));
            };

            dateInfo.ItemsSource = foods;
            fileManager = new FileManager();



            int TranslateDayOfWeek(int index)
            {
                if (index == 0)
                {
                    return 6;
                }
                else
                {
                    index--;
                    return index;
                }
            }
            DateTime moment = DateTime.Now;
            CultureInfo myCI = new CultureInfo("sv-SE");

            Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            int year = moment.Year;


            InitializeComponent();

            Title = "Kalender";

            Grid dayGrid = new Grid()
            {
                Padding = new Thickness(30, 0)
            };

            for (int i = 0; i < 7; i++)
            {
                dayGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                Label label = new Label()
                {
                    HeightRequest = 200,
                    WidthRequest = 50,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    Text = days[i]
                };
                dayGrid.Children.Add(label, i, 0);
            }


            StackLayout monthLayout = new StackLayout()
            {
                Spacing = 0,
            };

            for (int j = 0; j < 12; j++)
            {
                int currMonth = j;

                Label lbl = new Label
                {
                    FontSize = 25,
                    TextColor = Color.Gray,
                    Text = months[j],
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };

                Grid calenderGrid = new Grid()
                {
                    Margin = 30,
                    Padding = 5

                };

                for (int i = 0; i < 7; i++)
                {
                    calenderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                }

                for (int i = 0; i < 6; i++)
                {
                    calenderGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                }

                int daysInMonth = DateTime.DaysInMonth(year, currMonth + 1);
                int weekOfMonth = 0;
                for (int i = 0; i < daysInMonth; i++)
                {


                    DateTime date = new DateTime(year, currMonth + 1, 1).AddDays(i);


                    if (date.DayOfWeek == DayOfWeek.Monday)
                    {
                        weekOfMonth++;
                    }

                    Button button = new Button
                    {
                        Text = date.Day.ToString(),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = 60,
                        WidthRequest = 100,
                        BackgroundColor = Color.Gray

                    };
                    button.Clicked += delegate { DateSelected(date); dateSave = date; };


                    if (date.Month == moment.Month)
                    {
                        if (date.Day == DateTime.Now.Day)
                        {
                            button.BackgroundColor = Color.LightGreen;
                        }
                        else
                        {
                            button.BackgroundColor = Color.LightGray;
                        }

                    }
                    int dOFweek = TranslateDayOfWeek((int)myCal.GetDayOfWeek(date));

                    calenderGrid.Children.Add(button, dOFweek, weekOfMonth);
                }
                monthLayout.Children.Add(lbl);
                monthLayout.Children.Add(calenderGrid);
            }

            ScrollView scrollView = new ScrollView();
            scrollView.Content = monthLayout;


            Grid ParentGrid = new Grid
            {
                RowSpacing = 10
            };

            DateSelected(moment);

            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.2, GridUnitType.Star) });
            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1.5, GridUnitType.Star) });

            ParentGrid.Children.Add(dayGrid, 0, 0);
            ParentGrid.Children.Add(scrollView, 0, 1);
            ParentGrid.Children.Add(bottomLayout, 0, 2);

            Content = ParentGrid;
        }

        void DateSelected(DateTime selected)
        {
            List<Food> foodItems = fileManager.FoodsAtDate(selected);
            dateLabel.Text = selected.Year + " / " + months[selected.Month - 1] + " / " + selected.Day;
            foods = new ObservableCollection<Food>(foodItems);
            dateInfo.ItemsSource = foods;
        }

        void AddFood(DateTime selected)
        {
            Food food = new Food(selected.Date, "", "");
            Navigation.PushAsync(new FoodEditPage(food));
        }

    }
}