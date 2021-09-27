using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MaranataBlank.Models;
using MaranataBlank.Services;
using Xamarin.Forms;

namespace MaranataBlank.ViewModels
{
    class LocationExplorerViewModel : BaseViewModel
    {
        private string _searchText;
        public string searchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                if (string.IsNullOrEmpty(_searchText))
                {
                    searchPredictions = new ObservableCollection<SearchPrediction>(); 
                } else
                {
                    GetPlacesPredictionCommand.Execute(_searchText);
                }
                OnPropertyChanged();
            }
        }

        private ObservableCollection<SearchPrediction> _searchPredictions;
        public ObservableCollection<SearchPrediction> searchPredictions
        {
            get => _searchPredictions;
            set
            {
                _searchPredictions = value;
                OnPropertyChanged();
            }
        }

        private IGoogleMapsApiServices googleMapsApiServices = new GoogleMapsApiServices(); 
        public ICommand GetPlacesPredictionCommand { get; set; }

        public async Task GetPlacesPrediction(string keyword)
        {
            Console.WriteLine("Searching: " + keyword);
            // TODO: Throttle limitting to avoid exhausting the API.
            List<SearchPrediction> predictions = await googleMapsApiServices.GetPlacesPredictionFromKeyword(keyword);
            searchPredictions = new ObservableCollection<SearchPrediction>(predictions); 
        }

        public LocationExplorerViewModel()
        {
            searchPredictions = new ObservableCollection<SearchPrediction>();

            GetPlacesPredictionCommand = new Command<string>(async (param) => { await GetPlacesPrediction(param); });

            //searchPredictions.Add(new SearchPrediction("Location 1"));
            //searchPredictions.Add(new SearchPrediction("Location 2"));
            //searchPredictions.Add(new SearchPrediction("Location 2"));
        }
        
    }
}
