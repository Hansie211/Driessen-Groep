using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackend {
    public static class PasswordSecurity {

        private static RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();

        private static string HashToStr( byte[] data ) {

            StringBuilder builder = new StringBuilder();

            foreach ( byte b in data ) {

                builder.Append( b.ToString( "X2" ) );
            }

            return builder.ToString();
        }

        private static string ByteArrayToString( byte[] array ) {

            return Convert.ToBase64String( array );
        }

        private static byte[] StringToByteArray( string array ) {

            return Convert.FromBase64String( array );
        }

        private static byte[] GenerateSalt() {

            byte[] salt = new byte[32];
            RNG.GetBytes( salt, 0, salt.Length );

            return salt;
        }

        private static byte[] ConcatByteArrayAndString( byte[] bytes, string str ) {

            byte[] strbytes = Encoding.UTF8.GetBytes( str );

            byte[] output = new byte[ bytes.Length + strbytes.Length ];
            Array.Copy( bytes, 0, output, 0, bytes.Length );
            Array.Copy( strbytes, 0, output, bytes.Length, strbytes.Length );

            return output;
        }

        private static string GenerateHash( byte[] salt, string password ) {

            using ( SHA256 sha256Hash = SHA256.Create() ) {

                byte[] input    = ConcatByteArrayAndString( salt, password );
                byte[] data     = sha256Hash.ComputeHash( Encoding.UTF8.GetBytes( salt + password ) );
                return HashToStr( data );
            }
        }

        public static void SetPassword( string password, User user ) {

            byte[] salt = GenerateSalt();

            user.PasswordSalt = ByteArrayToString( salt );
            user.PasswordHash = GenerateHash( salt, password );
        }

        public static bool ComparePassword( string password, User user ) {

            string datapassword = GenerateHash( StringToByteArray( user.PasswordSalt ), user.PasswordHash );
            return password == datapassword;
        }
    }
}
