using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaranataBlank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationStatsExplorerView : ContentView
    {
        public LocationStatsExplorerView()
        {
            InitializeComponent();
        }
        
        //
        // Element manipulating functions
        //
        public void CloseBottomDrawer()
        {

        }

        //
        // Event handlers
        // 
        public void BackdropTapped(object sender, EventArgs e)
        {
            Console.WriteLine("Backdrop is tapped!"); 
        }

        public void BottomDrawerPanUpdated(object sender, EventArgs e)
        {

        }
    }
}