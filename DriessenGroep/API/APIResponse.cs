using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DriessenGroep {
    public static partial class API {
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
    }
}