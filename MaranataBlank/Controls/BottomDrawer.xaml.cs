using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaranataBlank.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [ContentProperty("ContainerContent")]
    public partial class BottomDrawer : Frame
    {
        private int appHeight
        {
            get => (int)this.Height;
            set
            {

            }
        }

        //private double closeY = 660;
        private double closeY
        { 
            get
            {
                // If we have a valid app height, then closeY is equal to it. 
                // Else, set to 1000, to make sure the frame is never seen.
                //
                // Limitation: IsDrawerHandleVisible is considered ignored unless 
                // appHeight is valid. 
                return appHeight > 0 ? appHeight - (IsDrawerHandleVisible ? 25 : 0) : 10000;
            }
        }
        private double openY
        {
            get
            {
                return appHeight > 0 ? appHeight / 2 : ((Device.RuntimePlatform == "Android") ? 260 : 280); 
            }
        }
        private double lastPanY = 0;
        private uint duration = 100;

        //
        // Bindable properties 
        // 

        // Boolean bindable property that determines whether the handle of 
        // the drawer should be visible. 
        public static readonly BindableProperty IsDrawerHandleVisibleProperty =
            BindableProperty.Create(
                propertyName: nameof(IsDrawerHandleVisible),
                returnType: typeof(bool),
                declaringType: typeof(BottomDrawer),
                defaultBindingMode: BindingMode.TwoWay, 
                propertyChanged: IsDrawerHandleVisisblePropertyChanged
        );

        public bool IsDrawerHandleVisible
        {
            get => (bool)base.GetValue(IsDrawerHandleVisibleProperty);
            set
            {
                base.SetValue(IsDrawerHandleVisibleProperty, value);
            }
        }

        public static void IsDrawerHandleVisisblePropertyChanged(
            BindableObject bindable,
            object oldValue,
            object newValue
        )
        {
            //Console.WriteLine("IsDrawerHandleVisible Changed: " + newValue); 
            // Manipulate drawer frames here 
            //((BottomDrawer)bindable).closeY = (bool)newValue ? 635 : 660;
        }

        // Boolean bindable property that determins whether the drawer should 
        // open or not. 
        public static readonly BindableProperty IsDrawerVisibleProperty =
            BindableProperty.Create(
                propertyName: nameof(IsDrawerVisible), 
                returnType: typeof(bool), 
                declaringType: typeof(BottomDrawer), 
                defaultBindingMode: BindingMode.TwoWay, 
                propertyChanged: IsDrawerVisiblePropertyChanged
            );
        
        public bool IsDrawerVisible
        {
            get => (bool)base.GetValue(IsDrawerVisibleProperty); 
            set
            {
                base.SetValue(IsDrawerVisibleProperty, value); 
            }
        }

        public static async void IsDrawerVisiblePropertyChanged(
            BindableObject bindable, 
            object oldVal, 
            object newVal
        )
        {
            Console.WriteLine("IsDrawerVisibleProperty changed!"); 
            if ((bool)newVal == true)
            {
                // Open drawer only if it it isn't already opened!
                await ((BottomDrawer)bindable).OpenDrawer();
            }
            else
            {
                await ((BottomDrawer)bindable).CloseDrawer();
            }
        }

        // Simulate a container like control 
        public View FrameContent
        {
            get { return bottomDrawerContent.Content;  }
            set { bottomDrawerContent.Content = value;  }
        }

        public BottomDrawer()
        {
            InitializeComponent();

            // GetScreenHeight(); 
        }

        public void GetScreenHeight()
        {
            bottomDrawer.VerticalOptions = LayoutOptions.End;
            bottomDrawer.HeightRequest = 100;
            InputTransparent = false;
            Console.WriteLine("Bottom coords: " + bottomDrawer.Y);
            Console.WriteLine("Drawer bg height: " + this.Height);
            InputTransparent = true; 
            bottomDrawer.VerticalOptions = LayoutOptions.FillAndExpand; 
        }

        public async Task OpenDrawer()
        {
            InputTransparent = false;
            //bottomDrawer.VerticalOptions = LayoutOptions.Fill;
            await bottomDrawer.TranslateTo(0, openY, duration);
            lastPanY = openY; 
        }

        public async Task CloseDrawer()
        {
            await bottomDrawer.TranslateTo(0, closeY, duration);
            InputTransparent = true;
            lastPanY = closeY;
            IsDrawerVisible = false;
        }

        //
        // Event handlers 
        // 
        public bool IsBackdropEnabled { get; set; } = true;
        public async void BackgroundFrameTapped(object sender, EventArgs e)
        {
            if (IsBackdropEnabled == true)
                IsDrawerVisible = false;
        }

        public async void BackgroundFramePinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            BackgroundFrameTapped(sender, new EventArgs()); 
        }

        public async void BackgroundFramePanUpdated(object sender, PanUpdatedEventArgs e)
        {
            BackgroundFrameTapped(sender, new EventArgs());
        }

        public async void DrawerPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Running)
            {
                // Clicks on the backdorp will not close the drawer while the drawer is being moved
                IsBackdropEnabled = false;
                double newPanY = lastPanY + e.TotalY;
                if (newPanY > 2 && newPanY < closeY)
                {
                    bottomDrawer.TranslationY = newPanY;
                    lastPanY = newPanY;
                }

                // Set drawer to be visible, but don't attempt to open the drawer. 
                //if (IsDrawerVisible == false)
                //    IsDrawerVisible = true;
            }
            else if (e.StatusType == GestureStatus.Completed) {

                if (lastPanY > ((closeY - openY) * 0.50) + openY)
                {
                    // Drawer has been dragged too low, close it. 
                    await CloseDrawer();
                    return; 
                }

                if (lastPanY < (openY * 0.70))
                {
                    // Drawer has been dragged past openY, drag it all the way to the top 
                    await bottomDrawer.TranslateTo(0, 2, duration);
                }

                // Enable backdrop 
                IsBackdropEnabled = true;
                IsDrawerVisible = true;
            }
        }
    }
}