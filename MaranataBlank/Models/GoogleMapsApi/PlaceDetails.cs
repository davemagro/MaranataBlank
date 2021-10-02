using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaranataBlank.Models.GoogleMapsApi
{
    public class PlaceDetails
    {
        public string Name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string Raw { get; set; }

        public static PlaceDetails FromJsonObject(JObject jsonObj)
        {
            // Create PlaceDetails instance from given json object.
            PlaceDetails placeDetails = new PlaceDetails();

            try
            {
                placeDetails.Name = (string)jsonObj["result"]["name"];
                placeDetails.latitude = (double)jsonObj["result"]["geometry"]["location"]["lat"];
                placeDetails.longitude = (double)jsonObj["result"]["geometry"]["location"]["lng"];
                placeDetails.Raw = (string)jsonObj.ToString(); 
            }
            catch
            {
                return null; 
            }

            return placeDetails; 
        }
    }

    public class PlaceDetailsResult
    {
        /*
        public List<AddressComponent> address_components { get; set; }
        public string adr_address { get; set; }
        public string business_status { get; set; }
        public string formatted_address { get; set; }
        public string formatted_phone_number { get; set; }
        public Geometry geometry { get; set; }
        public string icon { get; set; }
        public string icon_background_color { get; set; }
        public string icon_mask_base_uri { get; set; }
        public string international_phone_number { get; set; }
        public string name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public List<Photo> photos { get; set; }
        public string place_id { get; set; }
        public PlusCode plus_code { get; set; }
        public double rating { get; set; }
        public string reference { get; set; }
        public List<Review> reviews { get; set; }
        public List<string> types { get; set; }
        public string url { get; set; }
        public int user_ratings_total { get; set; }
        public int utc_offset { get; set; }
        public string vicinity { get; set; }
        public string website { get; set; } */
    }

    public class PlaceDetailsApiResult
    {
        public string Status { get; set; }
        public PlaceDetailsResult result { get; set; }
    }
}
