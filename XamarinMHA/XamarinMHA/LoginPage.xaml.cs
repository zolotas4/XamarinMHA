using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace HelloWorld
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        async void RegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage ());
        }

        async void LoginButtonClicked(object sender, EventArgs e)
        {
            HttpClient oHttpClient = new HttpClient();
            string url = "http://10.0.3.2:8080/people/search/findByUserName?username=" + usernameEntry.Text;
            Debug.WriteLine(url);
            var response = await oHttpClient.GetStringAsync(url);
            Person user = JsonConvert.DeserializeObject<Person>(response);
            if (String.Equals(passwordEntry.Text, user.Password))
            {
                UserHomePage uh = new UserHomePage
                {
                    BindingContext = user
                };
                await Navigation.PushAsync(uh);
            } else
            {
                invalidPasswordLabel.IsVisible = true;
            }
                
        }
    }
}
