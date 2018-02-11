using AppointmentModel;
using MentorModel;
using Newtonsoft.Json;
using SessionModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MentorAppointmentsPage : ContentPage
    {
        private Mentor mentor;
        public MentorAppointmentsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Utilities.toggleSpinner(spinner);
            populateListWithAppointments();
            Utilities.toggleSpinner(spinner);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            mentor = (Mentor)BindingContext;
        }

        async void populateListWithAppointments()
        {
            SessionEmbeddedWrapper sessionsList = new SessionEmbeddedWrapper();
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "sessions/search/findByMentorGreaterThanOrderByDateAsc?mentor=" + mentor.UserName + "&date=" + DateTime.Now.ToString("yyyy-MM-dd");
            Debug.WriteLine(url);
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync());
            }
            AppointmentsList.ItemsSource = sessionsList.Embedded.Sessions;
        }

        async void AppointmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            UserProfileForApproval userProfileForApproval = new UserProfileForApproval
            {
                BindingContext = e.SelectedItem
            };
            await Navigation.PushAsync(userProfileForApproval);
        }
    }
}