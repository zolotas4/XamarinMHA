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
    public partial class UserHome : ContentPage
    {
        Person user;

        public UserHome()
        {
            InitializeComponent();
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person) BindingContext;
        }

        async void MyProfileImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            UserProfile userProfile = new UserProfile();
            userProfile.BindingContext = user;
            await Navigation.PushAsync(userProfile);
        }
    }
}