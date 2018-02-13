using Newtonsoft.Json;
using PeopleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static XamarinMHA.MentorHomePage;

namespace XamarinMHA
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MentorAppointmentDetails : ContentPage
	{
		public MentorAppointmentDetails ()
		{
			InitializeComponent ();

		}

        protected override async void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            TempSession tempSession = (TempSession) BindingContext;

            HttpClient oHttpClient = new HttpClient();
            string url = Utilities.LOCALHOST + "people/search/findByUserName?username=" + tempSession.Session.Person;
            HttpResponseMessage response = await oHttpClient.GetAsync(url);
            Person user = JsonConvert.DeserializeObject<Person>(await response.Content.ReadAsStringAsync());
            skypeId.Text += user.Skype;
            whatsAppId.Text += user.WhatsApp;
            messengerId.Text += user.Messenger;
            viberId.Text += user.Viber;
            if (user.DefaultSocial == "skype")
            {
                skypeId.FontAttributes = FontAttributes.Bold;
                skypeId.Text += " (preferred)";
            } else if (user.DefaultSocial == "messenger")
            {
                messengerId.FontAttributes = FontAttributes.Bold; 
                messengerId.Text += " (preferred)";
            } else if (user.DefaultSocial == "whatsApp")
            {
                whatsAppId.FontAttributes = FontAttributes.Bold;
                whatsAppId.Text += " (preferred)";
            } else if (user.DefaultSocial == "viber")
            {
                viberId.FontAttributes = FontAttributes.Bold;
                viberId.Text += " (preferred)";
            }
        }
    }
}