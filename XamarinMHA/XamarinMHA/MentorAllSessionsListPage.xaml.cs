﻿using MentorModel;
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
using static HelloWorld.MentorHomePage;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MentorAllSessionsListPage : ContentPage
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
            string url = "http://" + Utilities.LOCALHOST + ":8080/sessions/search/findSessionsBeforeToday?mentor=" + mentor.UserName +
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
                string url2 = "http://" + Utilities.LOCALHOST + ":8080/people/search/findByUserName?username=" + session.Person;
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