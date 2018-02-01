using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace XamarinMHA.Droid
{
    [Activity(Label = "XamarinMHA", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            var x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.DarkThemeResources);
            x = typeof(Xamarin.Forms.Themes.Android.UnderlineEffect);
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}

