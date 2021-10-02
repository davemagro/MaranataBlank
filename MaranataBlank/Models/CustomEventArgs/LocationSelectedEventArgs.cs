using System;
using System.Collections.Generic;
using System.Text;

namespace MaranataBlank.Models.CustomEventArgs
{



    public class LocationSelectedEventArgs : EventArgs
    {
        public LocationSelectedEventArgs(LocationGeoDetails geoDetails)
        {
            this.GeoDetails = geoDetails;
        }
        public LocationGeoDetails GeoDetails { get; private set; }
    }
}

