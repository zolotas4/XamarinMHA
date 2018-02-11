using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public TimeLeftPage(int totalFunding, int timeConsumed)
        {
            InitializeComponent();
            this.totalFunding = totalFunding;
            this.timeConsumed = timeConsumed;
            //totalAllowanceLabel.Text = (totalFunding / 60) + "h and " + (totalFunding % 60) + " mins";
            usedAllownaceLabel.Text = (timeConsumed / 60) + "h and " + (timeConsumed % 60) + " mins";
            int timeLeft = totalFunding - timeConsumed;
            timeLeftLabel.Text = (timeLeft / 60) + "h and " + (timeLeft % 60) + " mins";
            PieData pd = new PieData(timeConsumed, timeLeft);
            this.BindingContext = pd;

        }

        public class PieData
        {
            public PlotModel PieModel { get; set; }

            public PieData(int timeConsumed, int timeLeft)
            {
                PieModel = CreatePieChart(timeConsumed, timeLeft);
            }
            private PlotModel CreatePieChart(int timeConsumed, int timeLeft)
            {
                var model = new PlotModel {};
                model.IsLegendVisible = true;
                var ps = new PieSeries
                {
                    StrokeThickness = 1,
                    InsideLabelPosition = 1.5,
                    AngleSpan = 360,
                    StartAngle = 0,
                    OutsideLabelFormat = "",
                    TickHorizontalLength = 0.00,
                    TickRadialLength = 0.00
                };
                PieSlice slice1 = new PieSlice("Time Left: " + (timeLeft / 60) + "h and " + (timeLeft % 60) + " mins", timeLeft);
                String appColor = Application.Current.Resources["appColor"].ToString();
                String textColor = Application.Current.Resources["textColor"].ToString();
                slice1.Fill = OxyColor.Parse(appColor);
                PieSlice slice2 = new PieSlice("Time Used: " + (timeConsumed / 60) + "h and " + (timeConsumed % 60) + " mins", timeConsumed);
                slice2.Fill = OxyColor.Parse(textColor);
                ps.Slices.Add(slice1);
                ps.Slices.Add(slice2);
                model.Series.Add(ps);
                return model;
            }
        }
    }
}