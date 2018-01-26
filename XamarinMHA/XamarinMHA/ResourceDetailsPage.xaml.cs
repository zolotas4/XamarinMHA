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

namespace HelloWorld
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResourceDetailsPage : ContentPage
    { 
        Person user;
        Resource resource;

        public ResourceDetailsPage(Person user)
		{
			InitializeComponent ();
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
            String updateFavoriesUrl = Utilities.LOCALHOST + "person/updateFavorites/" + user.UserName + "/" + resourceId + "/";
            HttpResponseMessage response = await oHttpClient.GetAsync(updateFavoriesUrl);
            if (response.IsSuccessStatusCode)
            {
                favIconItem.Icon = "favFilled.png";
                user.FavoriteResources.Add(resourceId);
                DisplayAlert("Success", "Added to Favorites", "OK");
            }
        }

        private void DownloadButtonClicked(object sender, EventArgs e)
        {

        }
    }
}