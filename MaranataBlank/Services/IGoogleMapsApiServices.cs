using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MaranataBlank.Models;

namespace MaranataBlank.Services
{
    public interface IGoogleMapsApiServices
    { 
        Task<List<Models.GoogleMapsApi.SearchPrediction>> GetPlacesPredictionFromKeyword(string keyword);

        Task<Models.GoogleMapsApi.PlaceDetails> GetPlaceDetails(string placeId); 
    }
}
