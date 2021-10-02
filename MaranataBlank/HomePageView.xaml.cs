using MaranataBlank.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaranataBlank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageView : ContentPage
    {

        public HomePageView()
        {

            InitializeComponent();

            InitializeMapStyle();

            // Initially, the searchBar has to centered. 
            SearchBarToCenter();

            PageAwareExtension.AttachLifecycleToPage(this, OnAppearing, null);

            // InitializeMessagincCenterSubscribers();
        }

        //
        // Some crazy hack in an attempt to detect whether the view has finished loading.
        //
        private async void OnAppearing(object sender, EventArgs eventArgs)
        {
            // Attach an event handler on the map when the user or code tries to interact with the map. 
            // https://stackoverflow.com/questions/41842089/how-can-i-know-when-a-view-is-finished-rendering
            await Task.Delay(1);  // I'm kinda crazy. 
            map.CameraMoveStarted += MapFocused;
        }

        //
        // View/Elements/Widgets manipulating functions 
        //
        public void SearchBarToCenter()
        {
            // Positions the searchBar a little bit in the vertically at the center of the 
            // application. This also does a little visual customization to the searchBar. 
            double displayHeight = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Height /
                Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Density;
            double searchBarPosY = (displayHeight / 2) * 0.50;
            frameSearchBar.Margin = 10;
            frameSearchBar.CornerRadius = 25;
            frameSearchBar.TranslateTo(0, searchBarPosY);
        }

        public void SearchBarToTop()
        {
            // Positions searchBar at the upper most part of the application. 
            frameSearchBar.Margin = 0;
            frameSearchBar.CornerRadius = 0;
            frameSearchBar.TranslateTo(0, 0, 50);
        }

        private void InitializeMapStyle()
        {
            // Initialize the style of the map with the embedded style template. 

            var assembly = typeof(HomePageView).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"MaranataBlank.MapResources.MapStyle.json");
            string styleBuff;

            using (var reader = new System.IO.StreamReader(stream))
            {
                styleBuff = reader.ReadToEnd();
            }

            map.MapStyle = Xamarin.Forms.GoogleMaps.MapStyle.FromJson(styleBuff);
        }


        //
        // View/Elements/Widgets event handlers. 
        //
        public void SearchBarFocused(object sender, EventArgs e)
        {
            // If the searchBar receives focus, then it should be position at the top of 
            // the application.
            SearchBarToTop();
        }

        public void SearchBarUnfocused(object sender, EventArgs e)
        {
            // Position searchBar at the center if it's empty and looses focus.
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                SearchBarToCenter();
            }
        }

        public void MapFocused(object sender, EventArgs e)
        {
            SearchBarToTop();
        }
    }
}