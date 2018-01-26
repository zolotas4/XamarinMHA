using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using PeopleModel;
using MentorModel;

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

        async void MentorRegisterButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MentorRegisterPage());
        }

        async void LoginButtonClicked(object sender, EventArgs e)
        {
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "people/search/findByUserName?username=" + usernameEntry.Text;
            Debug.WriteLine(url);
            Utilities.toggleSpinner(spinner);
            var response = await oHttpClient.GetAsync(url);
            Utilities.toggleSpinner(spinner);
            if (response.IsSuccessStatusCode)
            {
                // Username found in users' DB.
                Person user = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
                if (String.Equals(passwordEntry.Text, user.Password))
                {
                    // User has register but not approved.
                    if (user.Approved == "true")
                    {
                        UserHomePage uh = new UserHomePage
                        {
                            BindingContext = user
                        };
                        await Navigation.PushAsync(uh);
                    } else
                    {
                        UploadVerificationDocumentPage page = new UploadVerificationDocumentPage
                        {
                            BindingContext = user
                        };
                        await Navigation.PushAsync(page);
                    }
                }
                else
                {
                    invalidPasswordLabel.IsVisible = true;
                }
            } else
            {
                url = Utilities.LOCALHOST + "mentors/search/findByUserName?username=" + usernameEntry.Text;
                response = await oHttpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Username found in mentors' DB.
                    Mentor mentor = JsonConvert.DeserializeObject<Mentor>(await response.Content.ReadAsStringAsync());
                    if (String.Equals(passwordEntry.Text, mentor.Password))
                    {
                        MentorHomePage mh = new MentorHomePage
                        {
                            BindingContext = mentor
                        };
                        await Navigation.PushAsync(mh);
                    }
                    else
                    {
                        invalidPasswordLabel.IsVisible = true;
                    }
                } else
                {
                    await DisplayAlert("Error", "Something went wrong: no user found or access to DB is not possible.", "OK");
                }
            }
        }
    }
}
