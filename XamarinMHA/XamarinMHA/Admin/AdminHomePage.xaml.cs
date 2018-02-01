using AdminModel;
using SessionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminHomePage : ContentPage
    {
        Admin admin;
        public AdminHomePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            admin = (Admin)BindingContext;
        }

        async void MyProfileImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            UserProfilePage userProfile = new UserProfilePage
            {
                BindingContext = admin
            };
            await Navigation.PushAsync(userProfile);
        }

        async void LogSessionsImageTapped(object sender, EventArgs args)
        {

            var imageSender = (Image)sender;
        }

        async void ApproveUsersImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            ApproveSubmittedUsersPage approveSubmittedUsersPage = new ApproveSubmittedUsersPage
            {
                BindingContext = admin
            };
            await Navigation.PushAsync(approveSubmittedUsersPage);
        }

        async void MyApointmentsImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
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