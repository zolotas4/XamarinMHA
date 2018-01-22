using DLToolkit.Forms.Controls;
using PeopleModel;
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
        Label previousTappedItem;
        Person user;

        public MakeAppointmentPageSecondStep(Person user)
        {
            InitializeComponent();
            this.user = user;
            Debug.WriteLine(user.LastName);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            List<int> slots = (List<int>) BindingContext;
            List<TempSlot> slotsInTimeFormat = new List<TempSlot>();
            foreach (int slot in slots)
            {
                slotsInTimeFormat.Add(new TempSlot(Utilities.FindTimeBaseOnSlotNumber(slot)));
            }
            //mentorAvailableSlotsList.ItemsSource = slotsInTimeFormat;
            mentorAvailableSlotsListFlow.FlowItemsSource = slotsInTimeFormat;
        }

        async private void bookAppointmentButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("Item Selected", ((TempSlot)mentorAvailableSlotsListFlow.FlowLastTappedItem).Time.ToString(), "Ok");

        }

        private void mentorAvailableSlotsListFlow_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            {
                if (e.SelectedItem == null)
                {
                    return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
                }
                if (previousTappedItem != null)
                {
                    previousTappedItem.BackgroundColor = Color.Transparent;
                }
                ((Label) e.SelectedItem).BackgroundColor = Color.FromHex("#449D44");
                previousTappedItem = (Label) e.SelectedItem;
                bookAppointment.IsEnabled = true;
                //((ListView)sender).SelectedItem = null; //uncomment line if you want to disable the visual selection state.
            }
        }
    }

    class TempSlot
    {
        public TempSlot(String time)
        {
            this.Time = time;
        }
        public String Time { get; set; }
    }
}