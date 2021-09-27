using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaranataBlank.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageView : ContentPage
    {
        
        public HomePageView()
        {
            InitializeComponent();
        }

        /*
        public void BackdropTapped(object sender, EventArgs e)
        {

        }

        public void BottomDrawerPanUpdated(object sender, Xamarin.Forms.PanUpdatedEventArgs e)
        {

        }

        public void searchPlace()
        {
            OpenDrawer(); 
        }

        public void OpenDrawer()
        {
            frameBottomDrawer.TranslateTo(0, frameBottomDrawer.HeightRequest); 
            // When the drawer is open, the backdrop should listen if it is being tapped 
            // for it to know if the user requested to close the bottom drawer. 
            boxViewBackDrop.InputTransparent = false;  
        }

        private void CloseDrawer()
        {

        }
        */
    }
}