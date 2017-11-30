﻿using System;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.IO;
using Plugin.Media.Abstractions;
using PeopleModel;
using Newtonsoft.Json;
using MentorModel;
using AppointmentModel;
using System.Text;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutKeyfort : ContentPage
    {
        MediaFile file = null;
        //string url = "http://" + Utilities.LOCALHOST + ":8080/mentors/";
        //string sContentType = "application/json";
        public AboutKeyfort()
        {
            InitializeComponent();
            //Mentor mentor = new Mentor("John", "Doe", "john.doe", "test", "j.d@test.com");
            //HttpClient oHttpClient = new HttpClient();
            //var oTaskPostAsync = oHttpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(mentor), Encoding.UTF8, sContentType));
        }

        async void SelectImageButtonClicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + Utilities.LOCALHOST + ":8080/people/search/findByUserName?username=test";
            var response = await client.GetAsync(url);
            Person thanos = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            url = "http://" + Utilities.LOCALHOST + ":8080/mentors/search/findByUserName?username=mentor";
            response = await client.GetAsync(url);
            Mentor theMentor = JsonConvert.DeserializeObject<Mentor>(await response.Content.ReadAsStringAsync());
            DateTime start = new DateTime(2017, 11, 1);
            DateTime end = new DateTime(2017, 11, 2);
            Appointment theAppointment = new Appointment(thanos.UserName, theMentor.UserName, start, end);
            string postUrl = "http://" + Utilities.LOCALHOST + ":8080/appointments/";
            string sContentType = "application/json";
            Debug.WriteLine(JsonConvert.SerializeObject(theAppointment));
            
            HttpResponseMessage response2 = await client.PostAsync(postUrl, new StringContent(JsonConvert.SerializeObject(theAppointment), Encoding.UTF8, sContentType));
            Debug.WriteLine("Post Result: " + response2);
            /*
            await CrossMedia.Current.Initialize();
            file = await CrossMedia.Current.PickPhotoAsync(null);
            PersonImageHolder.Source = file.Path;
            */
            
        }

        async void UploadImageButtonClicked(object sender, EventArgs e)
        {
            
            /*
            String username = "thanos";
            HttpClient client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(file.GetStream()), "file", file.Path);
            HttpResponseMessage response = await client.PostAsync("http://" + Utilities.LOCALHOST + ":8080/photo/upload/" + username + "/", form);
            Debug.WriteLine(response.StatusCode);
            Debug.WriteLine(response.ReasonPhrase);
            Debug.WriteLine(response.ToString());
            */
        }

        /*
        async void RetrieveImageButtonClicked(object sender, EventArgs e)
        {
            String username = "thanos";
            HttpClient client = new HttpClient();
            //MultipartFormDataContent form = new MultipartFormDataContent();
            //form.Add(new StreamContent(file.GetStream()), "file", file.Path);
            HttpResponseMessage response = await client.GetAsync("http://" + Utilities.LOCALHOST + ":8080/photo/download/" + username + "/");
            Byte[] result = await response.Content.ReadAsByteArrayAsync();
            Debug.WriteLine(response.StatusCode);
            Debug.WriteLine(response.ReasonPhrase);
            Debug.WriteLine(response.ToString());
           
            RetrivedImageHolder.Source = ImageSource.FromStream(() => new MemoryStream(result));
        }
        */
    }

    
}