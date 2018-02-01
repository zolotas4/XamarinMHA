using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PeopleModel;
using MentorModel;
using AppointmentModel;
using SessionModel;

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
            string url = Utilities.LOCALHOST + "mentors/";
            var response = await oHttpClient.GetStringAsync(url);
            var mentorsList = JsonConvert.DeserializeObject<MentorEmbeddedWrapper>(response).Embedded.Mentors;
            Debug.WriteLine(mentorsList[1].FirstName);
            mentorPicker.ItemsSource = mentorsList;
            mentorPicker.ItemDisplayBinding = new Binding("FirstLastName");
            mentorPicker.SelectedIndex = 0;
        }

        async private void retrieveMentorPhotoAndPopulateImageHolder(String username)
        {
            HttpClient client = new HttpClient();
            //MultipartFormDataContent form = new MultipartFormDataContent();
            //form.Add(new StreamContent(file.GetStream()), "file", file.Path);
            HttpResponseMessage response = await client.GetAsync(Utilities.LOCALHOST + "photo/download/" + username + "/");
            Byte[] result = await response.Content.ReadAsByteArrayAsync();
            MentorPic.Source = ImageSource.FromStream(() => new MemoryStream(result));
        }

        private void mentorPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedMentorUsername = ((Mentor) mentorPicker.SelectedItem).UserName;
            retrieveMentorPhotoAndPopulateImageHolder(selectedMentorUsername);
        }

        async private void findSlotButtonClicked(object sender, EventArgs e)
        {
            Utilities.toggleSpinner(spinner);
            Mentor selectedMentor = (Mentor) mentorPicker.SelectedItem;

            HttpClient oHttpClient = new HttpClient();

            string url = Utilities.LOCALHOST + "sessions/search/findByMentorAndDate?mentor=" + selectedMentor.UserName +
                "&date=" + dateEntry.Date.ToString("yyyy-MM-dd");
            Debug.WriteLine("URL: " + url);
            HttpResponseMessage response = await oHttpClient.GetAsync(url);
            List<Session> sessionsList = new List<Session>();
            if (response.IsSuccessStatusCode)
            {
                sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Sessions;
            }
            List<int> availableSlots = new List<int>(Enumerable.Range(selectedMentor.startSlot, selectedMentor.endSlot - selectedMentor.startSlot + 1));
            foreach (Session session in sessionsList)
            {
                for (int i = session.startingSlotNumber; i <= session.startingSlotNumber + session.duration; i++)
                {
                    availableSlots.Remove(i);
                }
            }
            int duration = slotDurationPicker.SelectedIndex;
            if(duration == 2)
            {
                duration++;
            }
            List<int> filteredAvailableSlots = Utilities.filterAvailableSlotsBasedOnDuration(availableSlots, duration);
            MakeAppointmentPageSecondStep makeAppointmentPageSecondStep = new MakeAppointmentPageSecondStep(user, selectedMentor, dateEntry.Date, duration)
            {
                BindingContext = filteredAvailableSlots
            };
            Utilities.toggleSpinner(spinner);
            await Navigation.PushAsync(makeAppointmentPageSecondStep);
        }
    }
}