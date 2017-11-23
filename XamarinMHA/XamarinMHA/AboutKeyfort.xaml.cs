using Newtonsoft.Json;
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
    public partial class AboutKeyfort : ContentPage
    {
        //string url = "http://10.0.3.2:8080/mentors/";
        //string sContentType = "application/json";
        public AboutKeyfort()
        {
            InitializeComponent();
            //Mentor mentor = new Mentor("John", "Doe", "john.doe", "test", "j.d@test.com");
            //HttpClient oHttpClient = new HttpClient();
            //var oTaskPostAsync = oHttpClient.PostAsync(url, new StringContent(JsonConvert.SerializeObject(mentor), Encoding.UTF8, sContentType));
        }
    }
}