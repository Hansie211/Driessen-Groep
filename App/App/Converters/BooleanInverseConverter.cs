using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace App.Converters {
    class BooleanInverseConverter : IValueConverter {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {

            if ( !(value is bool ) ) {
                return false;
            }

            return !(bool)value;
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
