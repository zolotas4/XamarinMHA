using AppointmentModel;
using DLToolkit.Forms.Controls;
using MentorModel;
using Newtonsoft.Json;
using PeopleModel;
using SessionModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MakeAppointmentPageSecondStep : ContentPage
    {
        Label previousTappedItem;
        Person user;
        Mentor mentor;
        DateTime date;
        int duration;
        string url = "http://" + Utilities.LOCALHOST + ":8080/appointments/";
        string sessionUrl = "http://" + Utilities.LOCALHOST + ":8080/sessions/";
        string sContentType = "application/json";

        public MakeAppointmentPageSecondStep(Person user, Mentor mentor, DateTime date, int duration)
        {
            InitializeComponent();
            this.user = user;
            this.mentor = mentor;
            this.date = date;
            this.duration = duration;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            List<int> slots = (List<int>) BindingContext;
            List<TempSlot> slotsInTimeFormat = new List<TempSlot>();
            foreach (int slot in slots)
            {
                slotsInTimeFormat.Add(new TempSlot(Utilities.FindTimeBaseOnSlotNumber(slot)));
            }
            //mentorAvailableSlotsList.ItemsSource = slotsInTimeFormat;
            mentorAvailableSlotsListFlow.FlowItemsSource = slotsInTimeFormat;
        }

        async private void bookAppointmentButtonClicked(object sender, EventArgs e)
        {
            String selectedSlot = ((TempSlot)mentorAvailableSlotsListFlow.FlowLastTappedItem).Time.ToString();
            var answer = await DisplayAlert("Are you sure?", user.FirstName + ", you're about to book an appointment with " + mentor.FirstLastName 
                + " for " + date.Date.ToString("dd-MM-yyyy") + " @ " + selectedSlot + ". Do you want to proceed?", "Yes", "No");
            if (answer == true)
            {
                BookAppointmentPage bookAppointmentPage = new BookAppointmentPage(user, mentor, date);
                Utilities.toggleSpinner(spinner);
                HttpClient oHttpClient = new HttpClient();
                HttpResponseMessage response = new HttpResponseMessage();
                for (int i = 0; i <= duration; i++)
                {
                    Appointment appointment = new Appointment(user.UserName, mentor.UserName, date, Utilities.FindSlotNumberBasedOnTime(selectedSlot)+i);
                    response = await oHttpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, sContentType));
                }
                Session session = new Session(user.UserName, mentor.UserName, date, Utilities.FindSlotNumberBasedOnTime(selectedSlot), duration);
                response = await oHttpClient.PostAsync(sessionUrl, new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, sContentType));
                /*
                if (response.IsSuccessStatusCode)
                {
                    String sendEmailUrl = "http://" + Utilities.LOCALHOST + ":8080/email/send/" + user.Email + "/" + user.FirstName + "/";
                    response = await oHttpClient.GetAsync(sendEmailUrl);
                }
                */
                Utilities.toggleSpinner(spinner);
                await Navigation.PushAsync(bookAppointmentPage);
            }
        }

        private void mentorAvailableSlotsListFlow_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            {
                if (e.SelectedItem == null)
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }
                if (previousTappedItem != null)
                {
                    previousTappedItem.BackgroundColor = Color.Transparent;
                }
                ((Label) e.SelectedItem).BackgroundColor = Color.FromHex("#449D44");
                previousTappedItem = (Label) e.SelectedItem;
                bookAppointment.IsEnabled = true;
                //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            }
        }
    }

    class TempSlot
    {
        public TempSlot(String time)
        {
            this.Time = time;
        }
        public String Time { get; set; }
    }
}