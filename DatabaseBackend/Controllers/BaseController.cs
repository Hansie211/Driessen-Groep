using Microsoft.AspNetCore.Mvc;
using SharedLibrary;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatabaseBackend.Controllers {
    public abstract class BaseController : ControllerBase {

        private delegate bool TryParseHandler<T>( string value, out T result );

        private T GetClaim<T>( string claimType, TryParseHandler<T> handler, T @default = default( T ) ) {

            string value = User?.FindFirst( claimType )?.Value;
            if ( !handler( value, out T result ) ) {

                return @default;
            }

            return result;
        }

        private int? _AuthorizedID;
        private SecurityLevel? _AuthorizedSecurityLevel;

        protected readonly ApiContext Db;

        protected int AuthorizedID {
            get {

                if ( _AuthorizedID == null ) {

                    _AuthorizedID = GetClaim( ClaimTypes.NameIdentifier, int.TryParse, -1 );
                }

                return _AuthorizedID.Value;
            }
        }

        protected SecurityLevel AuthorizedSecurityLevel {
            get {

                if ( _AuthorizedSecurityLevel == null ) {

                    _AuthorizedSecurityLevel = GetClaim( ClaimTypes.Role, Enum.TryParse, SecurityLevel.User );
                }

                return _AuthorizedSecurityLevel.Value;
            }
        }
        
        
        public BaseController( ApiContext context ) : base() {

            Db = context;
        }

        private ObjectResult RequestResult( string message, int status, string title ) {

            return new ObjectResult( new { status, title, message } );
        }

        protected ObjectResult BadRequest( string message = "" ) {
            return RequestResult( message, 400, "Bad Request");
        }

        //protected ObjectResult Unauthorized( string message ) {

        //    return RequestResult( message, 401, "Unauthorized" );
        //}

        protected ObjectResult Forbidden( string message = "" ) {

            return RequestResult( message, 403, "Forbidden" );
        }

        protected ObjectResult NotFound( string message = "" ) { 

            return RequestResult( message, 404, "Not Found");
        }

        protected bool IsAuthorizedToAccess( int Id ) {

            if ( Id == AuthorizedID ) {

                return true;
            }

            return AuthorizedSecurityLevel >=  SecurityLevel.Administrator;
        }
    }
}
