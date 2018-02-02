using AppointmentModel;
using DLToolkit.Forms.Controls;
using MentorModel;
using Newtonsoft.Json;
using PeopleModel;
using Plugin.LocalNotifications;
using SessionModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MakeAppointmentPageSecondStep : ContentPage
    {
        Label previousTappedItem;
        Person user;
        Mentor mentor;
        DateTime date;
        int duration;
        string url = Utilities.LOCALHOST + "appointments/";
        string sessionUrl = Utilities.LOCALHOST + "sessions/";
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
                //We don't need to have duplicate info (both appointments and sessions). We can infer the first by having the second. Commented out so we don't save appointments.
                /*
                for (int i = 0; i <= duration; i++)
                {
                    Appointment appointment = new Appointment(user.UserName, mentor.UserName, date, Utilities.FindSlotNumberBasedOnTime(selectedSlot)+i);
                    response = await oHttpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, sContentType));
                }
                */
                Session session = new Session(user.UserName, mentor.UserName, Utilities.createDateTimeFromDateAndSlotNumber(date, Utilities.FindSlotNumberBasedOnTime(selectedSlot)), Utilities.FindSlotNumberBasedOnTime(selectedSlot), duration);
                response = await oHttpClient.PostAsync(sessionUrl, new StringContent(JsonConvert.SerializeObject(session), Encoding.UTF8, sContentType));

                if (response.IsSuccessStatusCode)
                {
                    //Set notifications
                    int notificationIdOne = Int32.Parse(Utilities.createNotificationIdFromDateAndSlot(date, Utilities.FindSlotNumberBasedOnTime(selectedSlot)) + "1");
                    int notificationIdTwo = Int32.Parse(Utilities.createNotificationIdFromDateAndSlot(date, Utilities.FindSlotNumberBasedOnTime(selectedSlot)) + "2");

                    CrossLocalNotifications.Current.Show("Appointment Notification", "You have an appointment scheduled for tomorrow.", notificationIdOne, Utilities.createDateTimeFromDateAndSlotNumber(date, Utilities.FindSlotNumberBasedOnTime(selectedSlot)).AddDays(-1));
                    CrossLocalNotifications.Current.Show("Appointment Notification", "You have an appointment strating in an hour.", notificationIdTwo, Utilities.createDateTimeFromDateAndSlotNumber(date, Utilities.FindSlotNumberBasedOnTime(selectedSlot)).AddMinutes(-60));

                    // Send Email
                    String sendEmailUrl = Utilities.LOCALHOST + "email/sendAppointmentConfirmation/" + user.Email + "/" + user.FirstName + "/" + mentor.FirstLastName
                + "%20for%20" + date.Date.ToString("dd-MM-yyyy") + "%20@%20" + selectedSlot + "/";
                    Debug.WriteLine(sendEmailUrl);
                    response = await oHttpClient.GetAsync(sendEmailUrl);
                }
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