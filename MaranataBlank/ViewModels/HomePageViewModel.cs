using MaranataBlank.Models;
using MaranataBlank.Models.GoogleMapsApi;
using MaranataBlank.Services;
using MaranataBlank.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MaranataBlank.ViewModels
{
    class HomePageViewModel : BaseViewModel
    {
        // 
        // Services to be used by the viewmodel; 
        // 
        private IGoogleMapsApiServices googleMapsApiServices;
        private IGeneralDataAnalysisServices dataAnalysisServices;

        private bool _drawerHandleVisible { get; set; } = false;
        private bool _drawerVisible { get; set; } = false;
        public bool DrawerHandleVisible
        {
            get => _drawerHandleVisible;
            set
            {
                _drawerHandleVisible = value;
                OnPropertyChanged();
            }
        }
        public bool DrawerVisible
        {
            get => _drawerVisible;
            set
            {
                _drawerVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingContent { get; set; } = false;

        public bool IsLoadingContent
        {
            get => _isLoadingContent;
            set
            {
                _isLoadingContent = value;
                OnPropertyChanged();
            }
        }


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
                    DrawerVisible = false;
                }
                else
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

        // This command is executed (1) every time the search query is changed;
        // (2) everytime the user executes the search command on the search field
        // (i.e. when the user clicks enter after inputing his query)
        public ICommand GetPlacesPredictionCommand { get; set; }

        public async Task GetPlacesPrediction(string keyword)
        {
            Console.WriteLine("Searching: " + keyword);
            // TODO: Throttle limitting to avoid exhausting the API.
            // TODO: Make sure the result updates the prediction in correct order. 
            // TODO: Exception occurs when messing with the search bar, especially with slow connection
            List<SearchPrediction> predictions = await googleMapsApiServices.GetPlacesPredictionFromKeyword(keyword);
            Console.WriteLine("eh");
            if (predictions != null)
            {
                searchPredictions = new ObservableCollection<SearchPrediction>(predictions);
            }
        }

        // The selected search prediction 
        private SearchPrediction _locationSelected { get; set; }
        public SearchPrediction LocationSelected
        {
            get => _locationSelected;
            set
            {
                _locationSelected = value;
                if (_locationSelected != null) {
                    PredictionSelectedCommand.Execute(_locationSelected);
                    searchPredictions = new ObservableCollection<SearchPrediction>();
                }
            }
        }

        // The location details to which the stats explorer will determine the 
        // statistics data to use in analysis. 
        private LocationGeoDetails _activeLocation { get; set; }
        public LocationGeoDetails ActiveLocation
        {
            get => _activeLocation;
            set
            {
                _activeLocation = value;
                OnPropertyChanged();
            }
        }

        // The statistics object to which the stats explorer rely on 
        private LocationStatistics _activeStatistics { get; set; }
        public LocationStatistics ActiveStatistics {
            get => _activeStatistics;
            set
            {
                _activeStatistics = value;
                OnPropertyChanged();
            }
        }

        public ICommand PredictionSelectedCommand { get; set; }
        public async Task PredictionSelected(SearchPrediction selectedPrediction)
        {
            // We update this property to notify the view elements binded to it about the 
            // change. 
            LocationGeoDetails _temp = LocationGeoDetails.FromSearchPrediction(selectedPrediction);
            // Mock API  
            _temp.Coordinates = new Coordinates();
            _temp.Coordinates.Latitude = 14.6840913;
            _temp.Coordinates.Longitude = 120.9921489;
            ActiveLocation = _temp;
            // Open drawer in full view 
            // So even if the drawer is closed, it's handle is still visible. 
            DrawerHandleVisible = true;     
            // We open the drawer just halfway. 
            DrawerVisible = true;
            // Show activity indicator
            IsLoadingContent = true;

            ActiveStatistics = new LocationStatistics(); 
            ActiveStatistics = await dataAnalysisServices.AnalyzeDataForLocation(ActiveLocation);
            IsLoadingContent = false;
        }

        

        public HomePageViewModel()
        {
            // Initialize services 
            //googleMapsApiServices = new GoogleMapsApiServices();
            googleMapsApiServices = new MockGoogleMapsApiServices();
            // dataAnalysisServices = new GeneralDataAnalysisServices(); 
            dataAnalysisServices = new MockGeneralDataAnalysisServices(); 

            // Initialize bindable properties 
            searchText = null;
            searchPredictions = new ObservableCollection<SearchPrediction>();
            ActiveStatistics = new LocationStatistics(); 

            // Initialize commands 
            GetPlacesPredictionCommand = new Command<string>(async param => await GetPlacesPrediction(param));
            PredictionSelectedCommand = new Command<SearchPrediction>(async param => await PredictionSelected(param));

        }
    }
}
