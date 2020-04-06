using System;
using System.Collections.Generic;
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
    public static class API {

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

        private static readonly int HostPort     = 44388;
        public static readonly string HostURL   = $"https://IPADDRESS:{ HostPort }";

        public class APIResponse<T> {

            public int ErrorCode { get; private set; }
            public Dictionary<string, string> ErrorMessage { get; private set; }

            public T Content { get; private set; }

            public bool Success { get; private set; }

            public static async Task<APIResponse<T>> Generate( HttpResponseMessage response ) {

                APIResponse<T> result = new APIResponse<T> {
                    ErrorCode    = (int)response.StatusCode,
                    Success      = response.IsSuccessStatusCode,
                };

                string responseBody = await response.Content.ReadAsStringAsync();

                if ( result.Success ) {

                    result.Content = JsonConvert.DeserializeObject<T>( responseBody );
                } else { 

                    if ( result.ErrorCode == 500 ) {

                        result.ErrorMessage = new Dictionary<string, string>() { { "message", responseBody } };
                    } else {
                        result.ErrorMessage = JsonConvert.DeserializeObject<Dictionary<string, string>>( responseBody );
                    }

                }

                return result;
            }

            public static APIResponse<T> GenerateException( Exception exp ) {

                return new APIResponse<T> {
                    ErrorCode    = -1,
                    Success      = false,

                    ErrorMessage = new Dictionary<string, string>() { { "message", exp.Message } }
                };
            }
        }

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
                
                HttpResponseMessage response = await client.PostAsync( "api/users/create/", JSONContent( user ) );
                return await APIResponse<User>.Generate( response );

            } catch ( Exception exp ) {

                return APIResponse<User>.GenerateException( exp );
            }
        }
    }
}