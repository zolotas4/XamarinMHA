using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        string url = "http://10.0.3.2:8080/people/";
        string sContentType = "application/json";

        public RegisterPage()
        {
            InitializeComponent();
        }

        async void CompleteRegistrationButtonClicked(object sender, EventArgs e)
        {
            Person person = new Person(firstNameEntry.Text, lastNameEntry.Text, usernameEntry.Text, passwordEntry.Text, emailEntry.Text, phoneEntry.Text, dobEntry.Date.Date.ToString());
            HttpClient oHttpClient = new HttpClient();
            var oTaskPostAsync = oHttpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, sContentType));
            
        }
    }
}