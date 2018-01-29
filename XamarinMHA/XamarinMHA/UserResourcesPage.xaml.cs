using Newtonsoft.Json;
using PeopleModel;
using ResourceModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class UserResourcesPage : TabbedPage
    {
        string sContentType = "application/json";

        Person user;
        ResourceEmbeddedWrapper allResources = new ResourceEmbeddedWrapper();
        ObservableCollection<Resource> favoriteResources = new ObservableCollection<Resource>();
        public UserResourcesPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person)BindingContext;
            Utilities.toggleSpinner(spinner);
            populateListWithFavoriteResources();
            populateListWithAllResources();
            Utilities.toggleSpinner(spinner);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void populateListWithFavoriteResources()
        {
            
            HttpClient oHttpClient = new HttpClient();
            foreach (String resourceId in user.FavoriteResources)
            {
                string url = Utilities.LOCALHOST + "resources/search/findById?id=" + resourceId;
                var response = await oHttpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    favoriteResources.Add(JsonConvert.DeserializeObject<Resource>(await response.Content.ReadAsStringAsync()));
                }
            }
            favoriteResourcesList.ItemsSource = favoriteResources;
        }

        async void populateListWithFavoriteResourcesRefreshUser()
        {

            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "people/search/findByUserName?username=" + user.UserName;
            Utilities.toggleSpinner(spinner);
            var response = await oHttpClient.GetAsync(url);
            Utilities.toggleSpinner(spinner);
            if (response.IsSuccessStatusCode)
            {
                user = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            }
            favoriteResources.Clear();

            foreach (String resourceId in user.FavoriteResources)
            {
                url = Utilities.LOCALHOST + "resources/search/findById?id=" + resourceId;
                response = await oHttpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    favoriteResources.Add(JsonConvert.DeserializeObject<Resource>(await response.Content.ReadAsStringAsync()));
                    Debug.WriteLine("Resource Id: " + resourceId);
                }
            }
            favoriteResourcesList.ItemsSource = favoriteResources;
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

            DetailedResourcePage resourceDetailsPage = new DetailedResourcePage(user);
            resourceDetailsPage.BindingContext = (Resource)e.Item; 
            await Navigation.PushAsync(resourceDetailsPage);
           
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

        private void searchFavoriteOnTextChanged(object sender, TextChangedEventArgs e)
        {
            
            favoriteResourcesList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                favoriteResourcesList.ItemsSource = favoriteResources;
            else
                favoriteResourcesList.ItemsSource = favoriteResources.Where(i => i.title.ToLower().Contains(e.NewTextValue.ToLower()) || i.shortDescription.ToLower().Contains(e.NewTextValue.ToLower()));
            favoriteResourcesList.EndRefresh();
            
        }

        private async Task FavoriteResourcesListRefreshing(object sender, EventArgs e)
        {
            Debug.WriteLine("Hi");
            populateListWithFavoriteResourcesRefreshUser();
            favoriteResourcesList.EndRefresh();
            //OnPropertyChanged("favoriteResourcesList");
        }

        async void AddToFavoritesImageTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
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
        }
    }
}