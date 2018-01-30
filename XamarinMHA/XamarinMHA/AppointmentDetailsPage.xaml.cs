using Newtonsoft.Json;
using PeopleModel;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static HelloWorld.UserProfilePage;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentDetailsPage : ContentPage
    {
        TempSessionMentor tempSession;
        public AppointmentDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            tempSession = (TempSessionMentor) BindingContext;
        }

        async private void cancelAppointmentButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "You're about to cancel the appointment. Do you want to proceed?", "Yes", "No");
            if (answer == true)
            {
                Utilities.toggleSpinner(spinner);
                //Delete session
                String sessionId = tempSession.Session.id;
                HttpClient oHttpClient1 = new HttpClient();
                string urlSession = Utilities.LOCALHOST + "session/updateSessions/remove/" + sessionId + "/";
                var response1 = await oHttpClient1.GetAsync(urlSession);
                
                //Delete notifications
                int notificationIdOne = Int32.Parse(Utilities.createNotificationIdFromDateAndSlot(tempSession.Session.date, tempSession.Session.startingSlotNumber) + "1");
                int notificationIdTwo = Int32.Parse(Utilities.createNotificationIdFromDateAndSlot(tempSession.Session.date, tempSession.Session.startingSlotNumber) + "2");
                CrossLocalNotifications.Current.Cancel(notificationIdOne);
                CrossLocalNotifications.Current.Cancel(notificationIdTwo);

                HttpClient oHttpClient = new HttpClient();
                string url = Utilities.LOCALHOST + "people/search/findByUserName?username=" + tempSession.Session.Person;
                var response = await oHttpClient.GetAsync(url);
                Utilities.toggleSpinner(spinner);
                if (response.IsSuccessStatusCode)
                {
                    Person user = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
                    UserHomePage uh = new UserHomePage
                    {
                        BindingContext = user
                    };
                    await Navigation.PushAsync(uh);
                }
            }
        }
    }
}