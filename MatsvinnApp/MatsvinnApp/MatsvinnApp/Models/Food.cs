using System;
using System.Collections.Generic;
using System.Text;

namespace MatsvinnApp.Models
{
    public class Food
    {
        public Food(DateTime date, string name, string caption)
        {
            Name = name;
            Caption = caption;
            Date = date;
        }

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
    }
}
