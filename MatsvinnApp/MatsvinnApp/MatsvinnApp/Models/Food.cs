using System;
using System.Collections.Generic;
using System.Text;

namespace MatsvinnApp.Models
{
    public class Food
    {
        public Food(string date, string name, string caption)
        {
            Name = name;
            Caption = caption;
            Date = date;
        }

        public string Date { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
    }
}
