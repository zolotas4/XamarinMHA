using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        Person user;
        public UserProfile()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person)BindingContext;
        }

        async void MyDetailsImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            UserDetails userDetails = new UserDetails();
            userDetails.BindingContext = user;
            await Navigation.PushAsync(userDetails);
        }
    }
}