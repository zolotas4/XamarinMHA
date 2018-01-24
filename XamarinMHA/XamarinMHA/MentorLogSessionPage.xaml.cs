using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static HelloWorld.MentorHomePage;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MentorLogSessionPage : ContentPage
    {
        TempSession tempSession;
        string sContentType = "application/json";
        public MentorLogSessionPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            tempSession = (TempSession)BindingContext;
            if (tempSession.Session.comments != "")
            {
                commentsEditor.Text = tempSession.Session.comments;
            }
        }

        private async Task logSessionButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "You're about to log the session. Do you want to proceed?", "Yes", "No");
            if (answer == true)
            {
                Utilities.toggleSpinner(spinner);
                HttpClient oHttpClient = new HttpClient();
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, tempSession.Session._links.self.href)
                {
                    Content = new StringContent("{ \"logged\": \"true\", \"comments\": \"" + commentsEditor.Text + "\" }", Encoding.UTF8, sContentType)
                };
                HttpResponseMessage response = await oHttpClient.SendAsync(request);
                Utilities.toggleSpinner(spinner);

                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("Done?", "Success", "Ok");
                    await Navigation.PopAsync();
                }
            }
        }
    }
}