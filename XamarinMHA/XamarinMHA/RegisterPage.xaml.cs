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
using PeopleModel;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        string url = "http://" + Utilities.LOCALHOST + ":8080/people/";
        string sContentType = "application/json";

        public RegisterPage()
        {
            InitializeComponent();
        }

        async void CompleteRegistrationButtonClicked(object sender, EventArgs e)
        {
            Person person = new Person(firstNameEntry.Text, lastNameEntry.Text, usernameEntry.Text, passwordEntry.Text, emailEntry.Text, phoneEntry.Text, dobEntry.Date.Date.ToString(), "false", "false");
            HttpClient oHttpClient = new HttpClient();
            HttpResponseMessage response = await oHttpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, sContentType));
            if (response.IsSuccessStatusCode)
            {
                String sendEmailUrl = "http://" + Utilities.LOCALHOST + ":8080/email/send/" + person.Email + "/" + person.FirstName + "/";
                response = await oHttpClient.GetAsync(sendEmailUrl);
                UploadVerificationDocumentPage page = new UploadVerificationDocumentPage
                {
                    BindingContext = person
                };
                await Navigation.PushAsync(page);
            }
        }
    }
}