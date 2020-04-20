using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace App.Converters {
    public class ShortDescriptionConverter : IValueConverter {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {

            string val = (value as string);
            if ( string.IsNullOrEmpty(val) ) { 
                return "";
            }

            const int cuttOffPoint = 50;

            if ( val.Length < cuttOffPoint + 3 ) {

                return val;
            }

            return val.Substring(0, cuttOffPoint ) + "...";
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
