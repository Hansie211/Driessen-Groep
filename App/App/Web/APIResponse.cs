using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App.Web {
    public class APIResponse<T> {

        public int ErrorCode { get; private set; }
        public Dictionary<string, string> ErrorMessage { get; private set; }

        public T Content { get; private set; }

        public bool IsSuccess { get; private set; }

        public static async Task<APIResponse<T>> Generate( HttpResponseMessage response ) {

            APIResponse<T> result = new APIResponse<T> {
                ErrorCode    = (int)response.StatusCode,
                IsSuccess      = response.IsSuccessStatusCode,
            };

            string responseBody = await response.Content.ReadAsStringAsync();

            if ( result.IsSuccess ) {

                if ( typeof( T ) == typeof( bool ) ) {

                    result.Content = (T)(object)true;
                } else if ( typeof( T ) == typeof( string ) ) {

                    result.Content = (T)(object)responseBody;
                } else {

                    result.Content = JsonConvert.DeserializeObject<T>( responseBody );
                }

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
                IsSuccess    = false,

                ErrorMessage = new Dictionary<string, string>() { { "message", exp.Message } }
            };
        }
    }
}