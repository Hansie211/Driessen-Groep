using Newtonsoft.Json;
using SharedLibrary;
using SharedLibrary.Models;
using SharedLibrary.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App.Web {

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

        private static readonly string AuthHeader   = "Authorization2";
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

            // Dummy token
            SetAccessToken( "eyJFbWFpbEFkZHJlc3MiOiJleGFtcGxlQGV4YW1wbGUuY29tIiwiVmFsaWRVbnRpbCI6IjIwMjEtMDQtMDdUMjE6MDI6MzMuMzc2MTY2OCswMjowMCIsIk5vbmNlIjoiYkVLSEVmK3hKSmJoWXdtVHVlYnU1b08yYlJPWFVPL3BaZmN1QU1KOGd3OD0iLCJIYXNoIjoiQzc3QkMxNkQ1RUI2NURFOTQ3OUYzRkJERURGOUI2OENCNkRBNjI4RUVFNjZGNUMyQTQyQjRGNzE4NUFDQTVBNCJ9" );
        }

        private static StringContent JSONContent( object content ) {

            string data = JsonConvert.SerializeObject( content );
            return new StringContent( data, Encoding.UTF8, "application/json" );
        }

        public static void SetAccessToken( string token ) {

            client.DefaultRequestHeaders.Remove( AuthHeader );
            client.DefaultRequestHeaders.Add( AuthHeader, token );
        }

        public static string GetAccessToken() {

            return client.DefaultRequestHeaders.GetValues( AuthHeader ).FirstOrDefault();
        }
        public static bool HasAccessToken() {

            return client.DefaultRequestHeaders.GetValues( AuthHeader ).Any();
        }

        private static async Task<APIResponse<T>> PostAsync<T>( string url, object content ) {

            try {

                HttpResponseMessage response = await client.PostAsync( url, JSONContent( content ) );
                return await APIResponse<T>.Generate( response );

            } catch ( Exception exp ) {

                return APIResponse<T>.GenerateException( exp );
            }
        }
        private static async Task<APIResponse<T>> PutAsync<T>( string url, object content ) {

            try {

                HttpResponseMessage response = await client.PutAsync( url, JSONContent( content ) );
                return await APIResponse<T>.Generate( response );

            } catch ( Exception exp ) {

                return APIResponse<T>.GenerateException( exp );
            }
        }
        private static async Task<APIResponse<T>> GetAsync<T>( string url ) {

            try {

                HttpResponseMessage response = await client.GetAsync( url );
                return await APIResponse<T>.Generate( response );

            } catch ( Exception exp ) {

                return APIResponse<T>.GenerateException( exp );
            }
        }
        private static async Task<APIResponse<T>> DeleteAsync<T>( string url ) {

            try {

                HttpResponseMessage response = await client.DeleteAsync( url );
                return await APIResponse<T>.Generate( response );

            } catch ( Exception exp ) {

                return APIResponse<T>.GenerateException( exp );
            }
        }


        public static async Task<APIResponse<string>> CreateUserAsync( string firstName, string lastName, string emailAddress, string password ) {

            CreateUserParameters parameters = new CreateUserParameters(){
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Password = password,
            };
            
            return await PostAsync<string>( "api/users", parameters );
        }

        public static async Task<APIResponse<string>> LoginUserAsync( string emailAddress, string password ) {

            LoginUserParameters parameters = new LoginUserParameters(){
                EmailAddress = emailAddress,
                Password = password,
            };

            return await PostAsync<string>( "api/users/login", parameters );
        }

        public static async Task<APIResponse<bool>> ChangeUserPasswordAsync( int id, string password ) {

            return await PutAsync<bool>($"api/users/{id}/password", password );
        }

        public static async Task<APIResponse<User>> DeleteUserAsync( int id ) {

            return await DeleteAsync<User>( $"api/users/{id}" );
        }


        public static async Task<APIResponse<IEnumerable<Event>>> GetEventsAsync() {

            return await GetAsync<IEnumerable<Event>>( "api/events ");
        }

        public static async Task<APIResponse<Event>> GetEventAsync( int id ) {

            return await GetAsync<Event>( $"api/events/{id}" );
        }

        public static async Task<APIResponse<Event>> CreateEventAsync( string title, DateTime date, string location, string description ) {

            Event @event = new Event(){
                Title = title,
                Date = date,
                Location = location,
                Description = description,
            };

            return await PostAsync<Event>( "api/events", @event );
        }

        public static async Task<APIResponse<Event>> UpdateEventAsync( int id, string title, DateTime date, string location, string description ) {

            Event @event = new Event(){
                Title = title,
                Date = date,
                Location = location,
                Description = description,
            };

            return await PutAsync<Event>( $"api/events/{id}", @event );
        }

        public static async Task<APIResponse<Event>> DeleteEventAsync( int id ) {

            return await DeleteAsync<Event>( $"api/events/{id}" );
        }

        public static async Task<APIResponse<bool>> SetEventOwnerAsync( int id, int userid, OwnershipLevel ownershipLevel ) {

            return await PostAsync<bool>( $"api/events/{id}/ownership/{userid}", ownershipLevel );
        }

        public static async Task<APIResponse<bool>> DeleteEventOwnerAsync( int id, int userid ) {

            return await DeleteAsync<bool>( $"api/events/{id}/ownership/{userid}" );
        }

        public static async Task<APIResponse<Speaker>> CreateSpeakerAsync( int id, string firstName, string lastName ) {

            Speaker speaker = new Speaker(){
                FirstName = firstName,
                LastName = lastName,
            };

            return await PostAsync<Speaker>( $"api/events/{id}/speakers", speaker );
        }

        public static async Task<APIResponse<Speaker>> UpdateSpeakerAsync( int id, int speakerid, string firstName, string lastName ) {

            Speaker speaker = new Speaker(){
                FirstName = firstName,
                LastName = lastName,
            };

            return await PutAsync<Speaker>( $"api/events/{id}/speakers/{speakerid}", speaker );
        }
        public static async Task<APIResponse<Speaker>> DeleteSpeakerAsync( int id, int speakerid ) {

            return await DeleteAsync<Speaker>( $"api/events/{id}/speakers/{speakerid}" );
        }
        public static async Task<APIResponse<Speaker>> CreateProgramAsync( int id, string title, string location, DateTime startTime, DateTime endTime, string description ) {

            EventProgram program = new EventProgram(){

                Title = title,
                Location = location,
                StartTime = startTime,
                EndTime = endTime,
                Description = description,
            };

            return await PostAsync<Speaker>( $"api/events/{id}/programs", program );
        }

        public static async Task<APIResponse<Speaker>> UpdateProgramAsync( int id, int programid, string title, string location, DateTime startTime, DateTime endTime, string description ) {

            EventProgram program = new EventProgram(){

                Title = title,
                Location = location,
                StartTime = startTime,
                EndTime = endTime,
                Description = description,
            };

            return await PostAsync<Speaker>( $"api/events/{id}/programs/{programid}", program );
        }
        public static async Task<APIResponse<Speaker>> DeleteProgramAsync( int id, int programid ) {

            return await DeleteAsync<Speaker>( $"api/events/{id}/programs/{programid}" );
        }
    }
}