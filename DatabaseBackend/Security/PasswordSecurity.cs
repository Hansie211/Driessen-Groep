using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackend.Security {
    public static class PasswordSecurity {

        private static string GenerateHash( byte[] salt, string password ) {

            using ( SHA256 sha256Hash = SHA256.Create() ) {

                byte[] input    = Encryption.ConcatBytes( salt, password );
                byte[] data     = sha256Hash.ComputeHash( input );
                return Encryption.HashToStr( data );
            }
        }

        public static void SetPassword( string password, User user ) {

            byte[] salt = Encryption.GenerateNonce(32);

            user.PasswordSalt = Encryption.ByteArrayToString( salt );
            user.PasswordHash = GenerateHash( salt, password );
        }

        public static bool ComparePassword( string password, User user ) {

            string datapassword = GenerateHash( Encryption.StringToByteArray( user.PasswordSalt ), password );
            return user.PasswordHash == datapassword;
        }
    }
}
