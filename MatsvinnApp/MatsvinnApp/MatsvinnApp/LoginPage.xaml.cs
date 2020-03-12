using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace MatsvinnApp
{
    public partial class LoginPage : ContentPage
    {
        string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public LoginPage()
        {
            InitializeComponent();
            string latestUserInfo = Path.Combine(saveFolder, "LatestUser.txt");

            if (File.Exists(latestUserInfo))
            {
                string[] lines = File.ReadAllLines(latestUserInfo);
                string[] dat = lines[0].Split('|');
                email.Text = dat[0];
                password.Text = dat[1];
                rememberBox.IsChecked = true;
            }
        }

        async void LoginButtonClicked(object sender, EventArgs args)
        {

            string loginData = Path.Combine(saveFolder, "UserData.txt");

            string mail = email.Text;
            string pass = password.Text;
            bool remember = rememberBox.IsChecked;
            if (File.Exists(loginData))
            {
                string[] lines = File.ReadAllLines(loginData);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] dat = lines[i].Split('|');
                    if (dat[0] == mail && dat[1] == pass)
                    {
                        await Navigation.PushAsync(new StudentMainPage { });
                    }
                }
            }
            else
            {
                File.WriteAllText(loginData, mail + '|' + pass);
            }

            string saveData = Path.Combine(saveFolder, "LatestUser.txt");

            if (remember)
            {
                File.WriteAllText(saveData, mail + '|' + pass);
            }
            else if (File.Exists(saveData))
            {
                File.Delete(saveData);
            }
        }
    }
}