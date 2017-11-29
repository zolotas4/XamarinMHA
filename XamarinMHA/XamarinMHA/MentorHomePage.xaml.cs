using MentorModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Debug.WriteLine(mentor.FirstLastName);
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

        async void ApproveUsersImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            ApproveSubmittedUsersPage approveSubmittedUsersPage = new ApproveSubmittedUsersPage
            {
                BindingContext = mentor
            };
            await Navigation.PushAsync(approveSubmittedUsersPage);
        }
    }
}