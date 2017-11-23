using Newtonsoft.Json;
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
    public partial class MakeAppointmentPage : ContentPage
    {
        Person user;
        public MakeAppointmentPage()
        {
            InitializeComponent();
            retrieveMentors();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person)BindingContext;
        }

        async void retrieveMentors()
        {
            HttpClient oHttpClient = new HttpClient();
            string url = "http://10.0.3.2:8080/mentors/";
            var response = await oHttpClient.GetStringAsync(url);
            var mentorsList = JsonConvert.DeserializeObject<MentorEmbeddedWrapper>(response).Embedded.Mentors;
            Debug.WriteLine(mentorsList[1].FirstName);
            mentorPicker.ItemsSource = mentorsList;
            mentorPicker.ItemDisplayBinding = new Binding("FirstLastName");
        }
    }
}