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

namespace XamarinMHA
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserHomePage : ContentPage
    {
        Person user;

        public UserHomePage()
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
            UserProfilePage userProfile = new UserProfilePage
            {
                BindingContext = user
            };
            await Navigation.PushAsync(userProfile);
        }

        async void MakeAnAppointmentImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "sessions/search/findByPerson?person=" + user.UserName;
            var response = await oHttpClient.GetStringAsync(url);
            var sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(response).Embedded.Sessions;
            var isTheFirstAppoinment = sessionsList.Count == 0;
            Debug.WriteLine("isTheFirst: " + isTheFirstAppoinment);
            MakeAppointmentPage makeAppointmentPage = new MakeAppointmentPage(isTheFirstAppoinment);
            makeAppointmentPage.BindingContext = user;
            await Navigation.PushAsync(makeAppointmentPage);
        }

        async void ResourcesImageTapped(object sender, EventArgs args)
        {
            //var imageSender = (Image)sender;
            UserResourcesPage userResourcesPage = new UserResourcesPage
            {
                BindingContext = user
            };
            await Navigation.PushAsync(userResourcesPage);
        }
    }
}