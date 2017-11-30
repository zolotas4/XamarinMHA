using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HelloWorld
{
    public static class Utilities
    {
        public static String LOCALHOST = "192.168.1.3";
        //public static String LOCALHOST = "10.0.3.2";
        public static void toggleSpinner (ActivityIndicator spinner) {
            spinner.IsVisible = !spinner.IsVisible;
            spinner.IsRunning = !spinner.IsRunning;
        }
    }
}
