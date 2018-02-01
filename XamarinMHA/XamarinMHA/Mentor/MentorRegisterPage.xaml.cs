using System;
using Plugin.Media;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using MentorModel;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MentorRegisterPage : ContentPage
    {
        string url = Utilities.LOCALHOST + "mentors/";
        string sContentType = "application/json";
        MediaFile file = null;

        public MentorRegisterPage()
        {
            InitializeComponent();
        }

        async void MentorImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            await CrossMedia.Current.Initialize();
            var action = await DisplayActionSheet("Select Method", "Cancel", null, "Gallery", "Camera");
            if (action == "Gallery")
            {
                file = await CrossMedia.Current.PickPhotoAsync(null);
            }
            else
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }
                Debug.WriteLine("Hi!");
                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                });
            }
            if (file == null)
                return;
            imageSender.Source = file.Path;
        }

        async void CompleteMentorRegistrationButtonClicked(object sender, EventArgs e)
        {
            Mentor mentor = new Mentor(firstNameEntry.Text, lastNameEntry.Text, usernameEntry.Text, passwordEntry.Text, emailEntry.Text, 9, 33);
            HttpClient oHttpClient = new HttpClient();
            var oTaskPostAsync = oHttpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(mentor), Encoding.UTF8, sContentType));
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(file.GetStream()), "file", file.Path);
            HttpResponseMessage response = await oHttpClient.PostAsync(Utilities.LOCALHOST + "photo/upload/" + usernameEntry.Text + "/", form);
            if (response.IsSuccessStatusCode)
            {
                await Navigation.PushAsync(new MentorRegistrationSuccesfulPage());
            }
            Debug.WriteLine(response.ReasonPhrase);
            Debug.WriteLine(response.ToString());
        }
    }
}