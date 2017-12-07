using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MakeAppointmentPageSecondStep : ContentPage
    {
        public MakeAppointmentPageSecondStep()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            List<int> slots = (List<int>) BindingContext;
            List<String> slotsInTimeFormat = new List<String>();
            foreach (int slot in slots)
            {
                slotsInTimeFormat.Add(Utilities.FindTimeBaseOnSlotNumber(slot));
            }
            mentorAvailableSlotsList.ItemsSource = slotsInTimeFormat;

        }

        async private void bookAppointmentButtonClicked(object sender, EventArgs e)
        {

        }
    }
}