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
    }
}