using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaranataBlank.ViewModels
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private string _searchText = ""; 

        public string searchText
        {
            get => _searchText; 
            set
            {
                _searchText = value;
                // SHOULD REQUEST AUTOCOMPLETE of _searchText 
                OnPropertyChanged();
            }
        }

        public HomePage()
        {
            InitializeComponent();
        }
    }
}