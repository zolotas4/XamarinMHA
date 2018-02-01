using PeopleModel;
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
    public partial class DetailedResourcePage : ContentPage
    {
        Person user;
        Resource resource;

        public DetailedResourcePage(Person user)
        {
            InitializeComponent();
            this.user = user;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            resource = (Resource)BindingContext;
            List<String> userFavs = user.FavoriteResources;
            Debug.WriteLine("User Favs: " + userFavs);
            if (userFavs.Contains(resource.id))
            {
                favIconItem.Icon = "favFilled.png";
            }
        }

        async void FavoriteButtonClicked(object sender, EventArgs e)
        {
            String resourceId = resource.id;
            HttpClient oHttpClient = new HttpClient();
            if (user.FavoriteResources.Contains(resourceId))
            {
                String removeFavoritesUrl = Utilities.LOCALHOST + "person/updateResources/favorites/remove/" + user.UserName + "/" + resourceId + "/";
                HttpResponseMessage response = await oHttpClient.GetAsync(removeFavoritesUrl);
                if (response.IsSuccessStatusCode)
                {
                    favIconItem.Icon = "favEmpty.png";
                    //user.FavoriteResources.Add(resourceId);
                    DisplayAlert("Success", "Removed from Favorites", "OK");
                }
            } else {
                String updateFavoritesUrl = Utilities.LOCALHOST + "person/updateResources/favorites/add/" + user.UserName + "/" + resourceId + "/";
                HttpResponseMessage response = await oHttpClient.GetAsync(updateFavoritesUrl);
                if (response.IsSuccessStatusCode)
                {
                    favIconItem.Icon = "favFilled.png";
                    //user.FavoriteResources.Add(resourceId);
                    DisplayAlert("Success", "Added to Favorites", "OK");
                }
            }
            
        }

        private void DownloadButtonClicked(object sender, EventArgs e)
        {

        }
        
    }
}

