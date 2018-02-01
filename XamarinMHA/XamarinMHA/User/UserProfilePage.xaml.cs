using AppointmentModel;
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
using static HelloWorld.MentorHomePage;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfilePage : ContentPage
    {
        Person user;
        public UserProfilePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person) BindingContext;
            Debug.WriteLine(user.GetType());
        }

        async void MyDetailsImageTapped(object sender, EventArgs args)
        {
            UserDetailsPage userDetails = new UserDetailsPage
            {
                BindingContext = user
            };
            await Navigation.PushAsync(userDetails);
        }

        async void MyAppointmentsImageTapped(object sender, EventArgs args)
        {
            Utilities.toggleSpinner(spinner);

            HttpClient oHttpClient = new HttpClient();

            string url = Utilities.LOCALHOST + "sessions/search/findByPersonGreaterThanOrderByDateDesc?person=" + user.UserName +
                "&date=" + DateTime.Now.ToString("yyyy-MM-dd");
            HttpResponseMessage response = await oHttpClient.GetAsync(url);
            List<Session> sessionsList = new List<Session>();
            if (response.IsSuccessStatusCode)
            {
                sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Sessions;
            }
            List<TempSessionMentor> formattedSessionsList = new List<TempSessionMentor>();
            
            foreach (Session session in sessionsList)
            {
                HttpClient oHttpClient2 = new HttpClient();

                string url2 = Utilities.LOCALHOST + "mentors/search/findByUserName?username=" + session.Mentor;
                HttpResponseMessage response2 = await oHttpClient.GetAsync(url2);
                Mentor mentor = JsonConvert.DeserializeObject<Mentor>(await response2.Content.ReadAsStringAsync());
                TempSessionMentor tempSession = new TempSessionMentor(mentor.FirstLastName, session.date.ToString("dd-MM-yyyy"), Utilities.FindTimeBaseOnSlotNumber(session.startingSlotNumber), session);
                formattedSessionsList.Add(tempSession);
            }
            MyAppointmentsUserPage myAppointments = new MyAppointmentsUserPage
            {
                BindingContext = formattedSessionsList
            };
            Utilities.toggleSpinner(spinner);
            await Navigation.PushAsync(myAppointments);
        }

        public class TempSessionMentor
        {
            public TempSessionMentor(String mentorFirstLastName, String date, String time, Session session)
            {
                this.MentorFirstLastName = mentorFirstLastName;
                this.Date = date;
                this.Time = time;
                this.Session = session;
            }

            public string MentorFirstLastName { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            internal Session Session { get; set; }
        }
    }
}