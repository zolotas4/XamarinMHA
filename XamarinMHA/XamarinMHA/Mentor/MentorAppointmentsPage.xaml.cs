using AppointmentModel;
using MentorModel;
using Newtonsoft.Json;
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
using static XamarinMHA.MentorHomePage;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MentorAppointmentsPage : ContentPage
    {
        public MentorAppointmentsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            List<TempSession> appointments = (List<TempSession>)BindingContext;
            foreach (TempSession s in appointments)
            {
                Debug.WriteLine(s.PersonFirstLastName);
            }
            AppointmentsList.ItemsSource = appointments;
        }

        async void AppointmentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MentorAppointmentDetails mentorAppointmentDetails = new MentorAppointmentDetails
            {
                BindingContext = e.SelectedItem
            };
            await Navigation.PushAsync(mentorAppointmentDetails);
        }
    }
}