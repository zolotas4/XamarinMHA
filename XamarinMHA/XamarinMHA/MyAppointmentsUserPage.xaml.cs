using AppointmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static HelloWorld.UserProfilePage;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAppointmentsUserPage : ContentPage
    {

        public MyAppointmentsUserPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            List<TempAppointment> appointments = (List<TempAppointment>) BindingContext;
            AppointmentsList.ItemsSource = appointments;
        }

        private async Task AppointmentItemTapped(object sender, ItemTappedEventArgs e)
        {
            AppointmentDetailsPage appointmentPage = new AppointmentDetailsPage
            {
                BindingContext = (TempAppointment) e.Item
            };
            await Navigation.PushAsync(appointmentPage);
        }
    }
}