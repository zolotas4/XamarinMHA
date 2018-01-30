using Newtonsoft.Json;
using PeopleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static HelloWorld.UserProfilePage;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentDetailsPage : ContentPage
    {
        TempSessionMentor tempAppointment;
        public AppointmentDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            tempAppointment = (TempSessionMentor) BindingContext;
        }

        async private void cancelAppointmentButtonClicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Are you sure?", "You're about to cancel the appointment. Do you want to proceed?", "Yes", "No");
            if (answer == true)
            {
                HttpClient oHttpClient = new HttpClient();
                string url = Utilities.LOCALHOST + "people/search/findByUserName?username=" + tempAppointment.Session.Person;
                Utilities.toggleSpinner(spinner);
                var response = await oHttpClient.GetAsync(url);
                Utilities.toggleSpinner(spinner);
                if (response.IsSuccessStatusCode)
                {
                    Person user = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
                    UserHomePage uh = new UserHomePage
                    {
                        BindingContext = user
                    };
                    await Navigation.PushAsync(uh);
                }
            }
        }
    }
}