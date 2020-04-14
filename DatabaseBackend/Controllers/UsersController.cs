using DatabaseBackend.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary;
using SharedLibrary.Models;
using SharedLibrary.Parameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

// https://restfulapi.net/rest-put-vs-post/

namespace DatabaseBackend.Controllers {

    [Route( "api/[controller]" )]
    [ApiController]
    [Authorize]
    public class UsersController : BaseController {
        public UsersController( ApiContext context ) : base( context ) {
        }

        // GET: api/users
        [HttpGet( "" )]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers() {

            return await Db.Users.ToListAsync();
        }

        // POST: api/users/
        [HttpPost( "" )]
        [AllowAnonymous]
        public async Task<ActionResult<string>> CreateUser( CreateUserParameters parameters ) {

            parameters.EmailAddress = parameters.EmailAddress.ToLower();

            if ( Db.Users.Where( o => o.Email == parameters.EmailAddress ).Any() ) {

                return BadRequest( "Dit emailadres is al in gebruik." );
            }

            User user = new User(){
                FirstName       = parameters.FirstName,
                LastName        = parameters.LastName,
                Email           = parameters.EmailAddress,
                SecurityLevel   = SecurityLevel.User,
            };

            PasswordSecurity.SetPassword( parameters.Password, user );

            Db.Users.Add( user );
            await Db.SaveChangesAsync();

            return AccessToken.Generate( user.Email );
        }

        // GET: api/users/5
        [HttpGet( "{id}" )]
        public async Task<ActionResult<User>> GetUser( int id ) {

            if ( !IsAuthorizedToAccess( id ) ) {

                return Forbidden( "Validation error." );
            }

            var user = await Db.Users.FindAsync(id);
            if ( user == null ) {

                return NotFound( "User not found." );
            }

            return user;
        }

        // PUT: api/users/5
        [HttpPut( "{id}" )]
        public async Task<ActionResult<User>> UpdateUser( int id, [FromBody]User userdata ) {

            if ( !IsAuthorizedToAccess( id ) ) {

                return Forbidden( "Validation error." );
            }

            User user = await Db.Users.FindAsync( id );
            if ( user == null ) {

                return NotFound( "User not found." );
            }

            user.CopyFromRequest( userdata );

            // Only admin or higher can upgrade, but a admin cannot go up to sysadmin
            if ( ( AuthorizedSecurityLevel >= SecurityLevel.Administrator ) && ( userdata.SecurityLevel > user.SecurityLevel ) && ( userdata.SecurityLevel <= AuthorizedSecurityLevel ) ) {

                user.SecurityLevel = userdata.SecurityLevel;
            }

            Db.Users.Update( user );
            await Db.SaveChangesAsync();

            return userdata;
        }

        // PUT: api/users/5/password
        [HttpPut( "{id}/password" )]
        public async Task<ActionResult> ChangeUserPassword( int id, [FromBody]string password ) {

            if ( !IsAuthorizedToAccess( id ) ) {

                return Forbidden( "Validation error." );
            }

            User user = await Db.Users.FindAsync( id );
            if ( user == null ) {

                return NotFound( "User not found." );
            }

            PasswordSecurity.SetPassword( password, user );

            Db.Users.Update( user );
            await Db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/users/5
        [HttpDelete( "{id}" )]
        public async Task<ActionResult<User>> DeleteUser( int id ) {

            if ( !IsAuthorizedToAccess( id ) ) {

                return Forbidden( "Validation error." );
            }

            var user = await Db.Users.FindAsync(id);
            if ( user == null ) {

                return NotFound( "User not found." );
            }

            Db.Users.Remove( user );
            await Db.SaveChangesAsync();

            return user;
        }

        // POST: api/users/login
        [HttpPost( "login" )]
        [AllowAnonymous]
        public async Task<ActionResult<string>> LoginUser( LoginUserParameters parameters ) {

            parameters.EmailAddress = parameters.EmailAddress.ToLower();

            var user = await Db.Users.FirstOrDefaultAsync( x => x.Email == parameters.EmailAddress );
            if ( user == null ) {

                return BadRequest( "Invalid emailadress / password" );
            }

            if ( !PasswordSecurity.ComparePassword( parameters.Password, user ) ) {

                return BadRequest( "Invalid emailadress / password" );
            }

            return AccessToken.Generate( user.Email );
        }

        private bool UserExists( int id ) {
            return Db.Users.Any( e => e.ID == id );
        }
    }
}
