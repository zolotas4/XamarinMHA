using Newtonsoft.Json;
using PeopleModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMHA.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UserSocialAccountsPage : ContentPage
	{
        Person user;
        string sContentType = "application/json";
        public UserSocialAccountsPage ()
		{
			InitializeComponent ();
		}

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person) BindingContext;
            Utilities.toggleSpinner(spinner);
            populateWithSocialMediaAsync();
            Utilities.toggleSpinner(spinner);
        }

        private async Task populateWithSocialMediaAsync()
        {
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "people/search/findByUserName?username=" + user.UserName;
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                user = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            }
            skypeEntry.Text = user.Skype;
            viberEntry.Text = user.Viber;
            messengerEntry.Text = user.Messenger;
            whatsAppEntry.Text = user.WhatsApp;
            if (user.DefaultSocial == "skype")
            {
                defaultSocialPicker.SelectedIndex = 0;
            } else if (user.DefaultSocial == "messenger")
            {
                defaultSocialPicker.SelectedIndex = 1;
            } else if (user.DefaultSocial == "whatsApp")
            {
                defaultSocialPicker.SelectedIndex = 2;
            } else if (user.DefaultSocial == "viber")
            {
                defaultSocialPicker.SelectedIndex = 3;
            }
        }
        private async Task SaveButtonClicked(object sender, EventArgs e)
        {
            HttpClient oHttpClient = new HttpClient();
            String defaultSocial = "";
            if (defaultSocialPicker.SelectedIndex == 0)
            {
                defaultSocial = "skype";
            } else if (defaultSocialPicker.SelectedIndex == 1)
            {
                defaultSocial = "messenger";
            } else if (defaultSocialPicker.SelectedIndex == 2)
            {
                defaultSocial = "whatsApp";
            } else if (defaultSocialPicker.SelectedIndex == 3)
            {
                defaultSocial = "viber";
            }
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, user._links.self.href)
            {
                Content = new StringContent("{ \"skype\": \"" + skypeEntry.Text + "\", \"messenger\": \"" + messengerEntry.Text + "\", \"whatsApp\": \"" + whatsAppEntry.Text + "\", \"viber\": \"" + viberEntry.Text + "\", \"defaultSocial\": \"" + defaultSocial + "\" }", Encoding.UTF8, sContentType)
            };
            HttpResponseMessage response = await oHttpClient.SendAsync(request);
            Utilities.toggleSpinner(spinner);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Saved", "Your accounts were updated successfully", "Ok");
                await Navigation.PopAsync();
            }
            Utilities.toggleSpinner(spinner);

        }
    }
}