using DatabaseBackend.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackend.Security {
    public class AccessToken {

        private static readonly string APP_SECRET       = "SECRET-{52E5FBA6-C9E0-4DCD-99BA-1AD226E95CEF}-{473D5902-2C67-45FA-A1BD-A3E6D38A5905}";
        private static readonly TimeSpan ValidDuration  = new TimeSpan( 1, 0, 0 );

        public string EmailAddress { get; set; }

        public DateTime ValidUntil { get; set; }

        public string Nonce { get; set; }

        public string Hash { get; set; }

        private string GenerateNonce() {

            return Encryption.ByteArrayToString( Encryption.GenerateNonce(32) );
        }
        private string GenerateHash() {

            using ( SHA256 sha256Hash = SHA256.Create() ) {

                byte[] input    = Encryption.ConcatBytes( APP_SECRET, EmailAddress, ValidUntil.ToString(), Nonce );
                byte[] data     = sha256Hash.ComputeHash( input );

                return Encryption.HashToStr( data );
            }
        }

        public AccessToken( string emailAddress ) {

            EmailAddress    = emailAddress;
            ValidUntil      = DateTime.Now.Add( ValidDuration );
            Nonce           = GenerateNonce();

            Hash            = GenerateHash();
        }

        public override string ToString() {

            string data = JsonConvert.SerializeObject( this );
            return Convert.ToBase64String( Encoding.UTF8.GetBytes( data ) );
        }

        public static explicit operator AccessToken( string data ) {

            data = Encoding.UTF8.GetString( Convert.FromBase64String( data ) );
            return JsonConvert.DeserializeObject<AccessToken>( data );
        }

        public bool IsValid() {

            if ( ValidUntil < DateTime.Now ) {

                return false;
            }

            return GenerateHash() == Hash;
        }

        public static string Generate( string email ) {

            AccessToken token = new AccessToken( email );
            return token.ToString();
        }

        public static bool IsTokenValid( string token ) {

            if ( string.IsNullOrEmpty(token) ) {

                return false;
            }

            try { 

                AccessToken accessToken = (AccessToken)token;
                return accessToken.IsValid();
            } catch {

                return false;
            }
        }
    }
}
