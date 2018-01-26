using Newtonsoft.Json;
using ResourceModel;
using System;
using System.Collections.Generic;
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
        public UserResourcesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //populateListWithFavoriteResources();
            Utilities.toggleSpinner(spinner);
            populateListWithAllResources();
            Utilities.toggleSpinner(spinner);
        }

        async void populateListWithFavoriteResources()
        {
            ResourceEmbeddedWrapper favoriteResources = new ResourceEmbeddedWrapper();
            HttpClient oHttpClient = new HttpClient();
            //TODO: Add correct REST url
            string url = Utilities.LOCALHOST + "resources/search/";
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                favoriteResources = JsonConvert.DeserializeObject<ResourceEmbeddedWrapper>(await response.Content.ReadAsStringAsync());
            }
            favoriteResourcesList.ItemsSource = favoriteResources.Embedded.Resources;
        }

        async void populateListWithAllResources()
        {
            ResourceEmbeddedWrapper allResources = new ResourceEmbeddedWrapper();
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "resources/";
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                allResources = JsonConvert.DeserializeObject<ResourceEmbeddedWrapper>(await response.Content.ReadAsStringAsync());
            }
            allResourcesList.ItemsSource = allResources.Embedded.Resources;
        }

        private void ResourceItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }

}