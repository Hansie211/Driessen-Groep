using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace App.Converters {
    public class DateConverter : IValueConverter {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {
            if ( !( value is DateTime ) ) {
                return null;
            }

            return ( (DateTime)value ).ToString( "dd/MM/yyyy H:mm" );
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
