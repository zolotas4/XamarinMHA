using MentorModel;
using Newtonsoft.Json;
using PeopleModel;
using SessionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static XamarinMHA.MentorHomePage;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MentorAllSessionsListPage : TabbedPage
    {
        Mentor mentor;
        public MentorAllSessionsListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            populateListsWithSessions();
        }

        async void populateListsWithSessions()
        {
            Utilities.toggleSpinner(spinner);
            HttpClient oHttpClient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            string url = Utilities.LOCALHOST + "sessions/search/findSessionsBeforeToday?mentor=" + mentor.UserName +
               "&date=" + DateTime.Now.ToString("yyyy-MM-dd");

            response = await oHttpClient.GetAsync(url);
            List<Session> sessionsList = new List<Session>();
            if (response.IsSuccessStatusCode)
            {
                sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Sessions;
            }
            List<TempSession> formattedNotLoggedSessionsList = new List<TempSession>();
            List<TempSession> formattedLoggedSessionsList = new List<TempSession>();

            foreach (Session session in sessionsList)
            {
                HttpClient oHttpClient2 = new HttpClient();
                string url2 = Utilities.LOCALHOST + "people/search/findByUserName?username=" + session.Person;
                HttpResponseMessage response2 = await oHttpClient.GetAsync(url2);
                Person person = JsonConvert.DeserializeObject<Person>(await response2.Content.ReadAsStringAsync());
                TempSession tempSession = new TempSession(person.FirstLastName, session.date.ToString("dd-MM-yyyy"), Utilities.FindTimeBaseOnSlotNumber(session.startingSlotNumber), session);
                if (session.logged)
                {
                    formattedLoggedSessionsList.Add(tempSession);
                }
                else
                {
                    formattedNotLoggedSessionsList.Add(tempSession);
                }
            }
            Utilities.toggleSpinner(spinner);
            LoggedSessionsList.ItemsSource = formattedLoggedSessionsList;
            OpenSessionsList.ItemsSource = formattedNotLoggedSessionsList;
        }

        
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            mentor = (Mentor)BindingContext;
        }

        private async Task SessionItemTapped(object sender, ItemTappedEventArgs e)
        {
            
            MentorLogSessionPage mentorLogSessionPage = new MentorLogSessionPage
            {
                BindingContext = (TempSession)e.Item
            };
            await Navigation.PushAsync(mentorLogSessionPage);
            
        }

    }
}