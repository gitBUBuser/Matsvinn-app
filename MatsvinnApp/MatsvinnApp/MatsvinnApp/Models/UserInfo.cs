using System;
using System.Collections.Generic;
using System.Text;

namespace MatsvinnApp.Models
{
    public class UserInfo
    {
        public UserInfo(string email, string password, bool isAdmin, List<DateTime> eatDates)
        {
            Email = email;
            Password = password;
            Admin = isAdmin;
            EatDates = eatDates;
        }

        public bool Admin { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<DateTime> EatDates { get; set; }
    }
}
