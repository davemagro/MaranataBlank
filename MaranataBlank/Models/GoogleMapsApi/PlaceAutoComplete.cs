//
// Data-Models for API responses on endpoint: BaseAddress + "api/place/autocomplete/json" 
// Documentation: https://developers.google.com/maps/documentation/places/web-service/autocomplete
// 

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaranataBlank.Models.GoogleMapsApi
{
    public class StructuredFormatting
    {
        [JsonProperty("main_text")]
        public string MainText { get; set; }

        [JsonProperty("secondary_text")]
        public string SecondaryText { get; set; }
    }

    public class SearchPrediction
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("structured_formatting")]
        public StructuredFormatting StructuredFormatting { get; set; }
    }

    public class AutoCompleteApiResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("predictions")]
        public List<SearchPrediction> AutoCompletePlaces { get; set; }
    }

}
