﻿using AdminModel;
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

        async void MentorRegistrationImageTapped(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new MentorRegisterPage());
        }

        async void ApproveUsersImageTapped(object sender, EventArgs args)
        {
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