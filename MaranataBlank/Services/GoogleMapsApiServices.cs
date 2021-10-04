using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

using MaranataBlank.Models.GoogleMapsApi;
using Newtonsoft.Json.Linq;

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
            long timestamp =  DateTimeOffset.Now.ToUnixTimeMilliseconds();
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
            Models.GoogleMapsApi.SearchPrediction _p = new SearchPrediction();
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

            string endpoint = "api/place/autocomplete/json";
            endpoint += "?input=" + Uri.EscapeUriString(keyword);
            endpoint += "&key=" + _apiKey;

            try
            {
                Console.WriteLine("Initiating request... " + _apiBaseAddress + endpoint);
                var resp = await _httpClient.GetAsync(endpoint).ConfigureAwait(false);
                Console.WriteLine("Request done!");
                if (resp.IsSuccessStatusCode)
                {
                    var json = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrEmpty(json))
                    {
                        //Console.WriteLine("Search Predictions JSON (\"" + keyword + "\"): " + json);
                        AutoCompleteApiResult result = await Task.Run(() =>
                            JsonConvert.DeserializeObject<AutoCompleteApiResult>(json)
                        ).ConfigureAwait(false);
                        if (result.Status == "OK")
                        {
                            predictions = result.AutoCompletePlaces;
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine("A PIECE OF SHIT EXCEPTION OCCURED! " + e); 
            }

            return predictions;

        }
        
        public async Task<PlaceDetails> GetPlaceDetails(string placeId)
        {
            string endpoint = "api/place/details/json";
            endpoint += "?place_id=" + Uri.EscapeUriString(placeId);
            endpoint += "&key=" + _apiKey;

            var resp = await _httpClient.GetAsync(endpoint); 
            if (resp.IsSuccessStatusCode)
            {
                var json = await resp.Content.ReadAsStringAsync(); 
                if (!string.IsNullOrEmpty(json))
                {
                    Console.WriteLine("Place details JSON (\"" + placeId + "\"): " + json);
                    // TODO: Handle response
                    // PlaceDetails placeDetails = PlaceDetails.FromJsonObject(JObject.Parse(json)); 
                }
            }

            return null; 
        }
    }
}
