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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void aboutUsImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            await Navigation.PushAsync(new AboutKeyfort());
        }

        async void loginImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            await Navigation.PushAsync(new LoginPage());
        }

        async void resourcesImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            await Navigation.PushAsync(new ResourcesPage());
        }

        async void contactUsImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            await Navigation.PushAsync(new ContactUsPage());
        }

        async void SosButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SosPage());
        }
    }
}