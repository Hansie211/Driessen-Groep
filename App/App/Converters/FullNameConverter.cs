using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace App.Converters {
    public class FullNameConverter : IValueConverter {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture ) {

            Person person = ( value as Person );
            if ( person == null ) {

                return "Onbekend";
            }

            return $"{person.FirstName} {person.LastName}";
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture ) {
            throw new NotImplementedException();
        }
    }
}
