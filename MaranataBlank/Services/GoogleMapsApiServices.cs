using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MaranataBlank.Models;
using Newtonsoft.Json;

namespace MaranataBlank.Services
{
    public class GoogleMapsApiServices : IGoogleMapsApiServices
    {
        private static string _apiKey; 
        private const string _apiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClient _httpClient;

        public GoogleMapsApiServices()
        {
            // Initialize http client 
            _httpClient = CreateClient();
        }

        public static void Initialize(string apiKey)
        {
            _apiKey = apiKey;
        }

        public HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress=new Uri(_apiBaseAddress)
            };
            // Accept only json 
            httpClient.DefaultRequestHeaders.Accept.Clear(); 
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            return httpClient;
        }

        public async Task<List<SearchPrediction>> GetPlacesPredictionFromKeyword(string keyword)
        {
            List<SearchPrediction> predictions = new List<SearchPrediction>();

            string endpoint = "api/place/autocomplete/json";
            endpoint += "?input=" + Uri.EscapeUriString(keyword);
            endpoint += "&key=" + _apiKey;

            var resp = await _httpClient.GetAsync(endpoint);
            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(json))
                {
                    Console.WriteLine("Search Predictions JSON: " + json);
                    // JsonSerializer.Deserialize();
                    GooglePlaceAutocompleteResult result = await Task.Run(() =>
                        JsonConvert.DeserializeObject<GooglePlaceAutocompleteResult>(json)
                    ); 
                    if (result.Status == "OK")
                    {
                        predictions = result.AutoCompletePlaces; 
                    }
                }
            }

            return predictions;
        }
    }
}
