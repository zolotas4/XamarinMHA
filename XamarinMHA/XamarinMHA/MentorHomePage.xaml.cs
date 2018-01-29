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

namespace HelloWorld
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
            var imageSender = (Image)sender;
            UserProfilePage userProfile = new UserProfilePage
            {
                BindingContext = mentor
            };
            await Navigation.PushAsync(userProfile);
        }

        async void LogSessionsImageTapped(object sender, EventArgs args)
        {
           
            var imageSender = (Image)sender;
            MentorAllSessionsListPage allSessionsListPage = new MentorAllSessionsListPage()
            {
                BindingContext = mentor
            };
            await Navigation.PushAsync(allSessionsListPage);
        }

        async void ApproveUsersImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            ApproveSubmittedUsersPage approveSubmittedUsersPage = new ApproveSubmittedUsersPage
            {
                BindingContext = mentor
            };
            await Navigation.PushAsync(approveSubmittedUsersPage);
        }

        async void MyApointmentsImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            MentorAppointmentsPage mentorAppointmentsPage = new MentorAppointmentsPage
            {
                BindingContext = mentor
            };
            await Navigation.PushAsync(mentorAppointmentsPage);
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