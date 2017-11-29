using PeopleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfileForApproval : ContentPage
    {
        Person user;
        string sContentType = "application/json";

        public UserProfileForApproval()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person)BindingContext;
        }

        async void ApproveButtonClicked(object sender, EventArgs e)
        {
            String username = user.UserName;
            HttpClient client = new HttpClient();
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, user._links.self.href)
            {
                Content = new StringContent("{ \"approved\": \"true\" }", Encoding.UTF8, sContentType)
            };
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", user.FirstLastName + " was approved. An email sent to the user.", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}