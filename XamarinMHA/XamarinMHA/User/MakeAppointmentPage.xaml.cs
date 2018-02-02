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

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MakeAppointmentPage : ContentPage
    {
        Person user;
        Mentor selectedMentor;
        List<Session> sessionsList = new List<Session>();
        List<int> filteredAvailableSlots = new List<int>();
        bool isTheFirstAppointment = false;
        int duration = 0;
        public MakeAppointmentPage(bool isTheFirstAppointment)
        {
            InitializeComponent();
            this.isTheFirstAppointment = isTheFirstAppointment;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person)BindingContext;
            if (!isTheFirstAppointment)
            {
                retrieveMentors();
            } else
            {
                // Disable the mentor picking elements as this is the first appointment and we should assing a random one.
                mentorSelectionContainer.IsVisible = false;
                selectMentorLabel.IsVisible = false;
            }

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


        async void findSessionList()
        {
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "sessions/search/findByMentorAndDate?mentor=" + selectedMentor.UserName +
            "&date=" + dateEntry.Date.ToString("yyyy-MM-dd");
            HttpResponseMessage response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Sessions;
            }
        }

        async private void findSlotButtonClicked(object sender, EventArgs e)
        {
            Utilities.toggleSpinner(spinner);
            if (!isTheFirstAppointment)
            {
                selectedMentor = (Mentor)mentorPicker.SelectedItem;
                List<int> availableSlots = new List<int>(Enumerable.Range(selectedMentor.startSlot, selectedMentor.endSlot - selectedMentor.startSlot + 1));
                Debug.WriteLine("Available Slots: " + availableSlots.Count());
                HttpClient oHttpClient = new HttpClient();
                string url = Utilities.LOCALHOST + "sessions/search/findByMentorAndDateBetween?mentor=" + selectedMentor.UserName +
                "&date1=" + dateEntry.Date.ToString("yyyy-MM-dd") + "&date2=" + dateEntry.Date.AddDays(1).ToString("yyyy-MM-dd");
                Debug.WriteLine(url);
                HttpResponseMessage response = await oHttpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Sessions;
                    foreach (Session session in sessionsList)
                    {
                        for (int i = session.startingSlotNumber; i <= session.startingSlotNumber + session.duration; i++)
                        {
                            Debug.WriteLine("Removed: " + i);
                            availableSlots.Remove(i);
                        }
                    }
                    duration = slotDurationPicker.SelectedIndex;
                    if (duration == 2)
                    {
                        duration++;
                    }
                    filteredAvailableSlots = Utilities.filterAvailableSlotsBasedOnDuration(availableSlots, duration);
                }
                if (filteredAvailableSlots.Count() == 0)
                {
                    DisplayAlert("Error", "The mentor you've selected has no available appointments for this day. Try another date, another mentor or pick smaller duration.", "OK");
                    Utilities.toggleSpinner(spinner);
                    return;
                }
            } else
            {
                Debug.WriteLine("Here!");
                HttpClient oHttpClient1 = new HttpClient();
                string url1 = Utilities.LOCALHOST + "mentors/";
                HttpResponseMessage response1 = await oHttpClient1.GetAsync(url1);
                List<Mentor> mentorsList = new List<Mentor>();
                if (response1.IsSuccessStatusCode)
                {
                    mentorsList = JsonConvert.DeserializeObject<MentorEmbeddedWrapper>(await response1.Content.ReadAsStringAsync()).Embedded.Mentors;
                }
                Random rnd = new Random();
                bool found = false;
                while (!found)
                {
                    int r = rnd.Next(mentorsList.Count);
                    selectedMentor = mentorsList[r];
                    List<int> availableSlots = new List<int>(Enumerable.Range(selectedMentor.startSlot, selectedMentor.endSlot - selectedMentor.startSlot + 1));
                    HttpClient oHttpClient = new HttpClient();
                    string url = Utilities.LOCALHOST + "sessions/search/findByMentorAndDateBetween?mentor=" + selectedMentor.UserName +
                    "&date1=" + dateEntry.Date.ToString("yyyy-MM-dd") + "&date2=" + dateEntry.Date.AddDays(1).ToString("yyyy-MM-dd");
                    HttpResponseMessage response = await oHttpClient.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        sessionsList = JsonConvert.DeserializeObject<SessionEmbeddedWrapper>(await response.Content.ReadAsStringAsync()).Embedded.Sessions;
                        foreach (Session session in sessionsList)
                        {
                            for (int i = session.startingSlotNumber; i <= session.startingSlotNumber + session.duration; i++)
                            {
                                availableSlots.Remove(i);
                            }
                        }
                        duration = slotDurationPicker.SelectedIndex;
                        if (duration == 2)
                        {
                            duration++;
                        }
                        filteredAvailableSlots = Utilities.filterAvailableSlotsBasedOnDuration(availableSlots, duration);
                        if (filteredAvailableSlots.Count() != 0)
                        {
                            Debug.WriteLine("Hi!");
                            found = true;
                            Debug.WriteLine(found);
                        }
                    }
                    Debug.WriteLine("Selected Mentor: " + selectedMentor.FirstLastName);
                }
                
            }
            Debug.WriteLine(filteredAvailableSlots.Count());

            MakeAppointmentPageSecondStep makeAppointmentPageSecondStep = new MakeAppointmentPageSecondStep(user, selectedMentor, dateEntry.Date, duration)
            {
                BindingContext = filteredAvailableSlots
            };
            Utilities.toggleSpinner(spinner);
            await Navigation.PushAsync(makeAppointmentPageSecondStep);
        }
    }
}