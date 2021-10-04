using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MaranataBlank.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DataView : StackLayout
    {
        public static readonly BindableProperty CaptionProperty = BindableProperty.Create(
            propertyName: nameof(Caption), 
            returnType: typeof(string), 
            declaringType: typeof(DataView), 
            propertyChanged: CaptionPropertyChanged
        );

        public String Caption
        {
            get => (string)(base.GetValue(CaptionProperty)); 
            set
            {
                base.SetValue(CaptionProperty, value); 
            }
        }

        public static void CaptionPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            ((DataView)bindable).CaptionControl.Text = (string)newVal;
        }


        public static readonly BindableProperty DataProperty = BindableProperty.Create(
            propertyName: nameof(Data),
            returnType: typeof(string),
            declaringType: typeof(DataView),
            propertyChanged: DataPropertyChanged
        );

        public String Data
        {
            get => (string)(base.GetValue(DataProperty));
            set
            {
                base.SetValue(DataProperty, value);
            }
        }

        public static void DataPropertyChanged(BindableObject bindable, object oldVal, object newVal)
        {
            ((DataView)bindable).DataControl.Text = (string)newVal;
        }



        public DataView()
        {
            InitializeComponent();
        }
    }
}