using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class UploadVerificationDocumentPage : ContentPage
    {
        MediaFile file;
        Person user;
        string sContentType = "application/json";

        public UploadVerificationDocumentPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person)BindingContext;
            if (user.Submitted == "true")
            {
                IntroText.Text = "We have received your verification form. We will email you when it has been approved.";
                UploadImageButton.IsVisible = false;
                SelectImageButton.IsVisible = false;
            }
        }

        async void SelectImageButtonClicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            file = await CrossMedia.Current.PickPhotoAsync(null);
            VerificationFormImageHolder.Source = file.Path;
            VerificationFormImageHolder.IsVisible = true;
            UploadImageButton.IsVisible = true;
        }

        async void UploadImageButtonClicked(object sender, EventArgs e)
        {
            String username = user.UserName + "VerificationForm";
            HttpClient client = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(new StreamContent(file.GetStream()), "file", file.Path);
            HttpResponseMessage response = await client.PostAsync("http://10.0.3.2:8080/photo/upload/" + username + "/", form);
            if (response.IsSuccessStatusCode)
            {
                HttpClient client2 = new HttpClient();
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, user._links.self.href)
                {
                    Content = new StringContent("{ \"submitted\": \"true\" }", Encoding.UTF8, sContentType)
                };
                HttpResponseMessage response2 = await client2.SendAsync(request);
                if (response2.IsSuccessStatusCode)
                {
                    IntroText.Text = "We have received your verification form. We will email you when it has been approved.";
                    UploadImageButton.IsVisible = false;
                    SelectImageButton.IsVisible = false;
                }
            }
        }
    }
}