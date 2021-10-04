using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

using MaranataBlank.Models;
using System.Threading.Tasks;

namespace MaranataBlank.Controls
{
    class ExtendedGoogleMaps : Xamarin.Forms.GoogleMaps.Map
    {
        public static readonly BindableProperty FocusedCoordinatesProperty =
            BindableProperty.Create(
              nameof(FocusedCoordinates), 
              typeof(Coordinates), 
              typeof(ExtendedGoogleMaps), 
              propertyChanged:FocusedCoordinatesPropertyChanged, 
              defaultBindingMode:BindingMode.TwoWay
            );

        public Coordinates FocusedCoordinates
        {
            get { return (Coordinates)GetValue(FocusedCoordinatesProperty); }
            set { SetValue(FocusedCoordinatesProperty, value); }
        }

        public static async void FocusedCoordinatesPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            ((ExtendedGoogleMaps)bindable).MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(((Coordinates)newVal).Latitude, ((Coordinates)newVal).Longitude),
                    Distance.FromMiles(10)
                )
            );
        }
    }
}
