using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SharedLibrary.Models;

namespace DriessenGroep {
    public static partial class API {

        private class FakeSSLHandler : HttpClientHandler {

            public FakeSSLHandler() {

                this.ClientCertificateOptions = ClientCertificateOption.Manual;
                this.ServerCertificateCustomValidationCallback = ( httpRequestMessage, cert, cetChain, policyErrors ) =>
                {
                    return true;
                };
            }
        }

        private static readonly HttpClient client = new HttpClient( new FakeSSLHandler() );

        private static readonly int HostPort        = 5000;
        private static readonly string HostProtocol = "http";
        private static readonly string HostAddress  = "192.168.178.10";
        public static readonly string HostURL       = $"{ HostProtocol }://{ HostAddress }:{ HostPort }";

        static API() {

            client.BaseAddress = new Uri( HostURL );
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue( "application/json" )
            );
        }

        private static StringContent JSONContent( object content ) {

            string data = JsonConvert.SerializeObject( content );
            return new StringContent( data, Encoding.UTF8, "application/json" );
        }

        public static async Task<APIResponse<User>> CreateUserAsync( string firstName, string lastName ) {

            User user = new User(){
                FirstName = firstName,
                LastName = lastName,
            };

            try {
                
                HttpResponseMessage response = await client.PostAsync( "api/users", JSONContent( user ) );
                return await APIResponse<User>.Generate( response );

            } catch ( Exception exp ) {

                return APIResponse<User>.GenerateException( exp );
            }
        }
    }
}