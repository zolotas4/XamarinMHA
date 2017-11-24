using System;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.IO;


namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutKeyfort : ContentPage
    {
        //string url = "http://10.0.3.2:8080/mentors/";
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
            String username = "thanos";
            await CrossMedia.Current.Initialize();
            var file = await CrossMedia.Current.PickPhotoAsync(null);
            PersonImageHolder.Source = file.Path;
            HttpClient client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(file.GetStream()), "file", file.Path);
            HttpResponseMessage response = await client.PostAsync("http://10.0.3.2:8080/photo/upload/" + username + "/", form);
            Debug.WriteLine(response.StatusCode);
            Debug.WriteLine(response.ReasonPhrase);
            Debug.WriteLine(response.ToString());
        }



    }

    
}