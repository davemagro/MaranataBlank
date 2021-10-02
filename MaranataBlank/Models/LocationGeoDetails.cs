using System;
using System.Collections.Generic;
using System.Text;

namespace MaranataBlank.Models
{

    public class Coordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class LocationGeoDetails
    {
        public string SecondaryText { get; set; }
        public Coordinates Coordinates { get; set; }
        public static LocationGeoDetails FromSearchPrediction(GoogleMapsApi.SearchPrediction searchPrediction)
        {
            LocationGeoDetails _temp = new LocationGeoDetails();
            _temp.SecondaryText = searchPrediction.StructuredFormatting.SecondaryText;
            
            return _temp;
        }
    }
}
