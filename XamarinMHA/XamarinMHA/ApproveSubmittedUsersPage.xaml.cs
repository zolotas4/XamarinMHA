using Newtonsoft.Json;
using PeopleModel;
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
    public partial class ApproveSubmittedUsersPage : ContentPage
    {
        public ApproveSubmittedUsersPage()
        {
            InitializeComponent();
            populateListWithSubmittedButNotApprovedUsers();
        }

        async void populateListWithSubmittedButNotApprovedUsers()
        {
            PeopleEmbeddedWrapper submittedButNotApprovedUsersList = new PeopleEmbeddedWrapper();
            HttpClient oHttpClient = new HttpClient();
            string url = "http://10.0.3.2:8080/people/search/findByApprovedAndSubmitted?approved=false&submitted=true";
            var response = await oHttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                submittedButNotApprovedUsersList = JsonConvert.DeserializeObject<PeopleEmbeddedWrapper>(await response.Content.ReadAsStringAsync());
            }
            SubmittedUsers.ItemsSource = submittedButNotApprovedUsersList.Embedded.People;
        }

        async void UserSelected(object sender, SelectedItemChangedEventArgs e)
        {
            UserProfileForApproval userProfileForApproval = new UserProfileForApproval
            {
                BindingContext = e.SelectedItem
            };
            await Navigation.PushAsync(userProfileForApproval);
        }
    }
}
