using MentorModel;
using PeopleModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookAppointmentPage : ContentPage
    {
        Person user;
        Mentor mentor;
        DateTime date;

        public BookAppointmentPage(Person user, Mentor mentor, DateTime date)
        {
            InitializeComponent();
            this.user = user;
            this.mentor = mentor;
            this.date = date;
            confirmationText.Text = user.FirstName + ", we've booked and appointment for you for " + date.Date.ToString("dd-MM-yyyy") + ". You will receive an email confirmation with all the details";
        }

        async private void backToHomeButtonClicked(object sender, EventArgs e)
        {
            UserHomePage uh = new UserHomePage
            {
                BindingContext = user
            };
            await Navigation.PushAsync(uh);
        }
    }
}