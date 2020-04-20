using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace App.Converters {
    public class TimeConverter : IValueConverter {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {

            if ( !(value is DateTime )){
                return null;
            }

            return ((DateTime)value).ToString( "H:mm" );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
