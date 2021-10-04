using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace MaranataBlank.Converters
{
    class WholeNumberfyPercentage: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double _val = System.Convert.ToDouble(value);
            if (Double.IsNaN(_val))
            {
                return value;
            }

            ulong _base = 100;
            while (true)
            {
                double i = _val * _base;
                if (i > 1)
                {
                    return i;
                }
                _base *= 10;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine("Converting back: " + value); 
            return value; 
        }
    }
}
