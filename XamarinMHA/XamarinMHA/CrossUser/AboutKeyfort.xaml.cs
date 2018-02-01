using System;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.IO;
using Plugin.Media.Abstractions;
using PeopleModel;
using Newtonsoft.Json;
using MentorModel;
using AppointmentModel;
using System.Text;
using Plugin.LocalNotifications;

namespace XamarinMHA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutKeyfort : ContentPage
    {
        public AboutKeyfort()
        {
            InitializeComponent();
        }
    }

    
}