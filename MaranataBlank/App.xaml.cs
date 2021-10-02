using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MaranataBlank.Views;
using MaranataBlank.Services;

namespace MaranataBlank
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            GoogleMapsApiServices.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new HomePageView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
