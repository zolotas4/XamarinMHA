using AppointmentModel;
using MentorModel;
using Newtonsoft.Json;
using PeopleModel;
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

        async void MyAppointmentsImageTapped(object sender, EventArgs args)
        {
            Utilities.toggleSpinner(spinner);

            HttpClient oHttpClient = new HttpClient();

            string url = "http://" + Utilities.LOCALHOST + ":8080/appointments/search/findByPersonGreaterThanOrderByDateDesc?person=" + user.UserName +
                "&date=" + DateTime.Now.ToString("yyyy-MM-dd");
            HttpResponseMessage response = await oHttpClient.GetAsync(url);
            List<Appointment> appointmentsList = new List<Appointment>();
            if (response.IsSuccessStatusCode)
            {
                appointmentsList = JsonConvert.DeserializeObject<AppointmentEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Appointments;
            }
            List<TempAppointment> formattedAppointmentsList = new List<TempAppointment>();
            
            foreach (Appointment appointment in appointmentsList)
            {
                HttpClient oHttpClient2 = new HttpClient();

                string url2 = "http://" + Utilities.LOCALHOST + ":8080/mentors/search/findByUserName?username=" + appointment.Mentor;
                HttpResponseMessage response2 = await oHttpClient.GetAsync(url2);
                Mentor mentor = JsonConvert.DeserializeObject<Mentor>(await response2.Content.ReadAsStringAsync());
                TempAppointment tempAppointment = new TempAppointment(mentor.FirstLastName, appointment.date.ToString("dd-MM-yyyy"), Utilities.FindTimeBaseOnSlotNumber(appointment.slotNumber), appointment);
                formattedAppointmentsList.Add(tempAppointment);
            }
            MyAppointmentsUserPage myAppointments = new MyAppointmentsUserPage
            {
                BindingContext = formattedAppointmentsList
            };
            Utilities.toggleSpinner(spinner);
            await Navigation.PushAsync(myAppointments);
        }

        public class TempAppointment
        {
            public TempAppointment(String mentorFirstLastName, String date, String time, Appointment appointment)
            {
                this.MentorFirstLastName = mentorFirstLastName;
                this.Date = date;
                this.Time = time;
                this.Appointment = appointment;
            }

            public string MentorFirstLastName { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            internal Appointment Appointment { get; set; }
        }
    }
}