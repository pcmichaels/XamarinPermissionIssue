using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinPermissionsIssue.Services;
using XamarinPermissionsIssue.Views;

namespace XamarinPermissionsIssue
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            if (status != PermissionStatus.Granted)
            {
                var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
            }

            status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                {
                    await Application.Current?.MainPage?.DisplayAlert("Need location", "Gunna need that location", "OK");
                }

                var permissions = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                if (!permissions.ContainsKey(Permission.LocationWhenInUse))
                {
                    // PermissionStatus.Unknown;
                }

                status = permissions.Select(a => a.Value).First();
            }

            if (status == PermissionStatus.Granted)
            {
                //Query permission
            }
            else if (status != PermissionStatus.Unknown)
            {
                //location denied
            }


        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
