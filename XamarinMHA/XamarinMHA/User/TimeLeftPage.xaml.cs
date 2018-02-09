using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinMHA.User
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimeLeftPage : ContentPage
	{
        int totalFunding = 0;
        int timeConsumed = 0;
        public TimeLeftPage (int totalFunding, int timeConsumed)
		{
			InitializeComponent ();
            this.totalFunding = totalFunding;
            this.timeConsumed = timeConsumed;
            totalAllowanceLabel.Text = (totalFunding / 60) + "h and " + (totalFunding % 60) + " mins";
            usedAllownaceLabel.Text = (timeConsumed / 60) + "h and " + (timeConsumed % 60) + " mins";
            int timeLeft = totalFunding - timeConsumed;
            timeLeftLabel.Text = (timeLeft / 60) + "h and " + (timeLeft % 60) + " mins";
        }

        
	}
}