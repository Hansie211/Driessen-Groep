using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DatabaseBackend.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedLibrary.Models;

namespace DatabaseBackend {
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions> {

        private readonly ApiContext Db;
        
        public BasicAuthenticationHandler( IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock, ApiContext context ) : base( options, logger, encoder, clock ) {

            Db = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync() {

            string token = Request.Headers["Authorization2"];

            if ( token == null ) {

                return AuthenticateResult.Fail("Missing AccessToken");
            }

            AccessToken accessToken = (AccessToken)token;
            if ( !accessToken.IsValid() ) {

                return AuthenticateResult.Fail( "Invalid AccessToken" );
            }

            User user = await Db.Users.FirstOrDefaultAsync( x => x.Email == accessToken.EmailAddress );
            if ( user == null ) {

                return AuthenticateResult.Fail( "No such user as in AccessToken" );
            }

            Claim[] claims = new[] {
                new Claim( ClaimTypes.Email,            accessToken.EmailAddress ),
                new Claim( ClaimTypes.NameIdentifier,   user.ID.ToString() ),
                new Claim( ClaimTypes.Role,             user.SecurityLevel.ToString() ),
            };

            var identity    = new ClaimsIdentity(claims, Scheme.Name);
            var principal   = new ClaimsPrincipal(identity);
            var ticket      = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success( ticket );
        }
    }
}
