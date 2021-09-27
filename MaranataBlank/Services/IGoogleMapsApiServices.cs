using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using MaranataBlank.Models;

namespace MaranataBlank.Services
{
    public interface IGoogleMapsApiServices
    { 
        Task<List<SearchPrediction>> GetPlacesPredictionFromKeyword(string keyword); 
    }
}
