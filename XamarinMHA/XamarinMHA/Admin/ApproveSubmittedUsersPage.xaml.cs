﻿using Newtonsoft.Json;
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

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ApproveSubmittedUsersPage : ContentPage
    {
        public ApproveSubmittedUsersPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Utilities.toggleSpinner(spinner);
            populateListWithSubmittedButNotApprovedUsers();
            Utilities.toggleSpinner(spinner);
        }

        async void populateListWithSubmittedButNotApprovedUsers()
        {
            PeopleEmbeddedWrapper submittedButNotApprovedUsersList = new PeopleEmbeddedWrapper();
            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "people/search/findByApprovedAndSubmitted?approved=false&submitted=true";
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
