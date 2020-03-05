using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MatsvinnApp
{
    public partial class StudentCalenderPage : ContentPage
    {
        public StudentCalenderPage()
        {
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

            int TranslateDayOfWeek(int index)
            {
                if(index == 0)
                {
                    return 6;
                }
                else
                {
                    index--;
                    return index;
                }
            }       

            DateTime moment = DateTime.Now.AddMonths(1);
            CultureInfo myCI = new CultureInfo("sv-SE");

            Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            int month = moment.Month - 1;
            int year = moment.Year;


            InitializeComponent();

            Title = "Kalender";

            ToolbarItem item = new ToolbarItem
            {
                Text = "Example",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            this.ToolbarItems.Add(item);

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
                int currMonth = j + 1;

                Label lbl = new Label
                {
                    WidthRequest = 100,
                    HeightRequest = 100,
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

                int daysInMonth = DateTime.DaysInMonth(year, currMonth);
                int weekOfMonth = 0;
                for (int i = 0; i < daysInMonth; i++)
                {
                   

                    DateTime date = new DateTime(year, currMonth, 1).AddDays(i);


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

                    if (date.Month == month)
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
            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.2, GridUnitType.Star) });
            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            ParentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        
            ParentGrid.Children.Add(dayGrid, 0, 0);
            ParentGrid.Children.Add(scrollView, 0, 1);
         
            Content = ParentGrid;
        }
      
    }
}