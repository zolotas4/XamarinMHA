using Newtonsoft.Json;
using ResourceModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourcesPage : ContentPage
    {
        ResourceEmbeddedWrapper allResources = new ResourceEmbeddedWrapper();

        public ResourcesPage()
        {
            InitializeComponent();
            Utilities.toggleSpinner(spinner);
            populateListWithAllResources();
            Utilities.toggleSpinner(spinner);
        }

        async void populateListWithAllResources()
        {

            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "resources/search/findByFree?free=true";
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                allResources = JsonConvert.DeserializeObject<ResourceEmbeddedWrapper>(await response.Content.ReadAsStringAsync());

            }
            resourcesList.ItemsSource = allResources.Embedded.Resources;
        }

        private void searchAllOnTextChanged(object sender, TextChangedEventArgs e)
        {
            resourcesList.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                resourcesList.ItemsSource = allResources.Embedded.Resources;
            else
                resourcesList.ItemsSource = allResources.Embedded.Resources.Where(i => i.title.ToLower().Contains(e.NewTextValue.ToLower()) || i.shortDescription.ToLower().Contains(e.NewTextValue.ToLower()));
            resourcesList.EndRefresh();
        }

        private async Task ResourceItemTapped(object sender, ItemTappedEventArgs e)
        {

            DetailedFreeResourcePage resourceDetailsPage = new DetailedFreeResourcePage();
            resourceDetailsPage.BindingContext = (Resource)e.Item;
            await Navigation.PushAsync(resourceDetailsPage);

        }
    }
}