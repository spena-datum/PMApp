﻿


namespace PMApp.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Android.Runtime;
    using Plugin.CurrentActivity;
    using Plugin.Permissions;

    [Activity(Label = "PMApp", 
        Icon = "@mipmap/icon", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);//Inicializando el plugin para tomar foto
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(
        int requestCode,
        string[] permissions,
        [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(
             requestCode,
             permissions,
             grantResults);
        }
    }
}