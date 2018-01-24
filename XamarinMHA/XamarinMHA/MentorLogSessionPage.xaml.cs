using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            if (tempSession.Session.actualDuration != 0)
            {
                durationHoursEntry.Text = (tempSession.Session.actualDuration / 60).ToString();
                durationMinsEntry.Text = (tempSession.Session.actualDuration % 60).ToString();
            }
        }

        private async Task logSessionButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "You're about to log the session. Do you want to proceed?", "Yes", "No");
            if (answer == true)
            {
                int actualDuration = Int32.Parse(durationHoursEntry.Text) * 60 + Int32.Parse(durationMinsEntry.Text);
                Debug.WriteLine("Actual Duration: " + actualDuration);
                Utilities.toggleSpinner(spinner);
                HttpClient oHttpClient = new HttpClient();
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, tempSession.Session._links.self.href)
                {
                    Content = new StringContent("{ \"logged\": \"true\", \"comments\": \"" + commentsEditor.Text + "\", \"actualDuration\": \"" + actualDuration + "\" }", Encoding.UTF8, sContentType)
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
        //TODO: Time left for user is not shown.
    }
}