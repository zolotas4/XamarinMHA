using Newtonsoft.Json;
using PeopleModel;
using ResourceModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static HelloWorld.MentorHomePage;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MentorLogSessionPage : TabbedPage
    {
        ResourceEmbeddedWrapper allResources = new ResourceEmbeddedWrapper();
        List<Resource> suggestedResources = new List<Resource>();

        TempSession tempSession;
        string sContentType = "application/json";
        Person user;
        public MentorLogSessionPage()
        {
            InitializeComponent();
            Utilities.toggleSpinner(spinner);
            populateListWithAllResources();
            Utilities.toggleSpinner(spinner);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            tempSession = (TempSession)BindingContext;
            if (tempSession.Session.comments != "")
            {
                commentsEditor.Text = tempSession.Session.comments;
            }
            if (tempSession.Session.actualDuration != 0)
            {
                durationHoursEntry.Text = (tempSession.Session.actualDuration / 60).ToString();
                durationMinsEntry.Text = (tempSession.Session.actualDuration % 60).ToString();
            }
        }

        private async Task logSessionButtonClicked(object sender, EventArgs e)
        {
           
            var answer = await DisplayAlert("Are you sure?", "You're about to log the session. Do you want to proceed?", "Yes", "No");
            if (answer == true)
            {
                foreach (Resource r in suggestedResources)
                {
                    HttpClient oHttpClient2 = new HttpClient();
                    String updateSuggestedUrl = Utilities.LOCALHOST + "person/updateResources/suggested/add/" + tempSession.Session.Person + "/" + r.id + "/";
                    Debug.WriteLine(updateSuggestedUrl);
                    HttpResponseMessage response2 = await oHttpClient2.GetAsync(updateSuggestedUrl);
                }

                int actualDuration = Int32.Parse(durationHoursEntry.Text) * 60 + Int32.Parse(durationMinsEntry.Text);
                Debug.WriteLine("Actual Duration: " + actualDuration);
                Utilities.toggleSpinner(spinner);
                HttpClient oHttpClient = new HttpClient();
                var method = new HttpMethod("PATCH");
                var request = new HttpRequestMessage(method, tempSession.Session._links.self.href)
                {
                    Content = new StringContent("{ \"logged\": \"true\", \"comments\": \"" + commentsEditor.Text + "\", \"actualDuration\": \"" + actualDuration + "\" }", Encoding.UTF8, sContentType)
                };
                HttpResponseMessage response = await oHttpClient.SendAsync(request);
                Utilities.toggleSpinner(spinner);
                if (response.IsSuccessStatusCode)
                {                  
                    DisplayAlert("Done?", "Success", "Ok");
                    await Navigation.PopAsync();
                }
            }
        }

        private void searchAllOnTextChanged(object sender, TextChangedEventArgs e)
        {
            allResourcesList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                allResourcesList.ItemsSource = allResources.Embedded.Resources;
            else
                allResourcesList.ItemsSource = allResources.Embedded.Resources.Where(i => i.title.ToLower().Contains(e.NewTextValue.ToLower()) || i.shortDescription.ToLower().Contains(e.NewTextValue.ToLower()));
            allResourcesList.EndRefresh();
        }

        async void populateListWithAllResources()
        {

            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "resources/";
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                allResources = JsonConvert.DeserializeObject<ResourceEmbeddedWrapper>(await response.Content.ReadAsStringAsync());
            }
            allResourcesList.ItemsSource = allResources.Embedded.Resources;
        }

        private async Task ResourceItemTapped(object sender, ItemTappedEventArgs e)
        {
            suggestedResources.Add((Resource)e.Item);
        }

        

        /*
        async void TickImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            if (imageSender.Source.Equals("unticked.png"))
            {
                imageSender.Source = "ticked.png";
            } else
            {
                imageSender.Source = "unticked.png";
            }
            Debug.WriteLine("Args: " + args);
            /*
            String resourceId = ((Resource)e.Item).id;
            HttpClient oHttpClient = new HttpClient();
            String updateFavoriesUrl = Utilities.LOCALHOST + "person/updateFavorites/" + user.UserName + "/" + resourceId + "/";
            HttpResponseMessage response = await oHttpClient.GetAsync(updateFavoriesUrl);
            if (response.IsSuccessStatusCode)
            {
                DisplayAlert("Success", "Added to Favorites", "OK");
            }
            */
        //}

        public ICommand TickImageClicked
        {
            get
            {
                return new Command((e) =>
                {
                var item = (e as Resource);
                    Debug.WriteLine(((Resource)e).title);
                });
            }
        }

        //TODO: Time left for user is not shown.
    }
}