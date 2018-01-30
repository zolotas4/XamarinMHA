using ResourceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HelloWorld
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailedFreeResourcePage : ContentPage
    {
        Resource resource;
        public DetailedFreeResourcePage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            resource = (Resource)BindingContext;
           
        }

        private void DownloadButtonClicked(object sender, EventArgs e)
        {

        }
    }
}