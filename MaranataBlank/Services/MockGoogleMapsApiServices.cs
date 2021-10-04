using MaranataBlank.Models.GoogleMapsApi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MaranataBlank.Services
{
    class MockGoogleMapsApiServices : IGoogleMapsApiServices
    {
        public async Task<List<SearchPrediction>> GetPlacesPredictionFromKeyword(string keyword)
        {
            SearchPrediction _p = new SearchPrediction();
            _p.Id = "SOME_PREDICTION_ID";
            _p.PlaceId = "SOME_PLACE_ID";
            _p.Reference = "EH";
            _p.StructuredFormatting = new StructuredFormatting();
            _p.StructuredFormatting.MainText = keyword;
            _p.StructuredFormatting.SecondaryText = keyword + ", Manila, Philippines";

            List<SearchPrediction> predictions = new List<SearchPrediction>
            {
                _p, _p, _p
            };

            await Task.Delay(100);

            return predictions; 
        }

        public async Task<PlaceDetails> GetPlaceDetails(string placeId)
        {
            return new PlaceDetails(); 
        }
    }
}
