using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetails : ContentPage
    {
        Person user;
        public UserDetails()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            user = (Person)BindingContext;
        }

        async void UpdateDetailsButtonClicked(object sender, EventArgs args)
        {
            Debug.WriteLine("Clicked...");
            //await Navigation.PushAsync(new UserDetails());
        }
    }
}