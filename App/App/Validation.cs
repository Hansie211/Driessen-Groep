using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace App {
    public static class Validation {

        public static Regex EmailRegex = new Regex (@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public static bool EmailAddress( string emailAddres ) {

            if ( string.IsNullOrWhiteSpace( emailAddres ) )
                return false;

            return EmailRegex.IsMatch( emailAddres );
        }
    }
}
