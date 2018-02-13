using MentorModel;
using Newtonsoft.Json;
using PeopleModel;
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
    public partial class MentorHomePage : ContentPage
    {
        Mentor mentor;

        public MentorHomePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            mentor = (Mentor)BindingContext;
        }

        async void MyProfileImageTapped(object sender, EventArgs args)
        {
            UserProfilePage userProfile = new UserProfilePage
            {
                BindingContext = mentor
            };
            await Navigation.PushAsync(userProfile);
        }

        async void LogSessionsImageTapped(object sender, EventArgs args)
        {
           
            MentorAllSessionsListPage allSessionsListPage = new MentorAllSessionsListPage()
            {
                BindingContext = mentor
            };
            await Navigation.PushAsync(allSessionsListPage);
        }

        async void ApproveUsersImageTapped(object sender, EventArgs args)
        {
        }

        async void MyApointmentsImageTapped(object sender, EventArgs args)
        {
            List<Session> sessionsList = new List<Session>();
            Utilities.toggleSpinner(spinner);
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "sessions/search/findByMentorGreaterThanOrderByDateAsc?mentor=" + mentor.UserName + "&date=" + DateTime.Now.ToString("yyyy-MM-dd");
            Debug.WriteLine(url);
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Sessions;
            }

            List<TempSession> formattedSessionsList = new List<TempSession>();

            foreach (Session session in sessionsList)
            {
                HttpClient oHttpClient2 = new HttpClient();

                string url2 = Utilities.LOCALHOST + "people/search/findByUserName?username=" + session.Person;
                HttpResponseMessage response2 = await oHttpClient.GetAsync(url2);
                Person user = JsonConvert.DeserializeObject<Person>(await response2.Content.ReadAsStringAsync());
                TempSession tempSession = new TempSession(user.FirstLastName, session.date.ToString("dd-MM-yyyy"), Utilities.FindTimeBaseOnSlotNumber(session.startingSlotNumber), session);
                formattedSessionsList.Add(tempSession);
            }
            MentorAppointmentsPage mentorAppointmentsPage = new MentorAppointmentsPage
            {
                BindingContext = formattedSessionsList
            };
            Utilities.toggleSpinner(spinner);
            await Navigation.PushAsync(mentorAppointmentsPage);
        }

        private async Task LogoutButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Logout", "Are you sure?", "Yes", "No");
            if (answer == true)
            {
                await Navigation.PopToRootAsync();
            }
        }

        public class TempSession
        {
            public TempSession(String personFirstLastName, String date, String time, Session session)
            {
                this.PersonFirstLastName = personFirstLastName;
                this.Date = date;
                this.Time = time;
                this.Session = session;
            }

            public string PersonFirstLastName { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            internal Session Session { get; set; }
        }
    }
}