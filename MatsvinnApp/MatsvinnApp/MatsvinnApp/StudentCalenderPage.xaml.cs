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
                "Måndag",
                "Tisdag",
                "Onsdag",
                "Torsdag",
                "Fredag",
                "Lördag",
                "Söndag"
            };

        

            DateTime moment = DateTime.Now;
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            int month = moment.Month;
            int year = moment.Year;

            int WeekOfMonth(DateTime date)
            {
                date = date.Date;
                DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
                DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
                if (firstMonthMonday > date)
                {
                    firstMonthDay = firstMonthDay.AddMonths(-1);
                    firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
                }
                int week = (date - firstMonthMonday).Days / 7 + 1;

                return week;

            }

            InitializeComponent();

            Title = "March";

            ToolbarItem item = new ToolbarItem
            {
                Text = "Example",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            this.ToolbarItems.Add(item);

            int daysInMonth = DateTime.DaysInMonth(year, month);

            Grid dayGrid = new Grid();
            for (int i = 0; i < 7; i++)
            {
                dayGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                Label label = new Label()
                {
                    Text = days[i]
                };
                dayGrid.Children.Add(label, i, 0);
            }

            Grid calenderGrid = new Grid();

            for (int i = 0; i < 7; i++)
            {
                calenderGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < 6; i++)
            {
                calenderGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < daysInMonth; i++)
            {
                DateTime date = new DateTime(year, month, 1).AddDays(i);

                Button button = new Button
                {
                    Text = date.Day.ToString(),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                int week = myCal.GetWeekOfYear(date, myCWR, myFirstDOW);
                calenderGrid.Children.Add(button, (int)myCal.GetDayOfWeek(date), WeekOfMonth(date));
                date.AddDays(i);
            }
            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(dayGrid);
            stackLayout.Children.Add(calenderGrid);


            Content = stackLayout;
        }
      
    }
}