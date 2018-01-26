using AppointmentModel;
using MentorModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld
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
            populateListWithAppointments();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            mentor = (Mentor)BindingContext;
        }

        async void populateListWithAppointments()
        {
            AppointmentEmbeddedWrapper appointmentsList = new AppointmentEmbeddedWrapper();
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "appointments/search/findByMentorOrderByStartingDateTimeAsc?mentor=" + mentor.UserName;
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                appointmentsList = JsonConvert.DeserializeObject<AppointmentEmbeddedWrapper>(await response.Content.ReadAsStringAsync());
            }
            AppointmentsList.ItemsSource = appointmentsList.Embedded.Appointments;
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