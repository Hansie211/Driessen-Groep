using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBackend.Security {
    public static class Encryption {

        private static RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();

        public static string HashToStr( byte[] data ) {

            StringBuilder builder = new StringBuilder();

            foreach ( byte b in data ) {

                builder.Append( b.ToString( "X2" ) );
            }

            return builder.ToString();
        }

        public static byte[] ConcatBytes( params object[] items ) {

            BinaryFormatter bf = new BinaryFormatter();
            using ( MemoryStream stream = new MemoryStream() ) {

                foreach( object obj in items ) {

                    if ( obj == null ) {
                        continue;
                    }

                    bf.Serialize( stream, obj );
                }

                return stream.ToArray();
            }
        }

        public static byte[] GenerateNonce( int size ) {

            byte[] data = new byte[size];
            RNG.GetBytes( data, 0, data.Length );

            return data;
        }

        public static string ByteArrayToString( byte[] array ) {

            return Convert.ToBase64String( array );
        }

        public static byte[] StringToByteArray( string array ) {

            return Convert.FromBase64String( array );
        }

    }
}
