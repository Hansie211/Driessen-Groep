using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseBackend;
using SharedLibrary.Models;
using SharedLibrary;

// https://restfulapi.net/rest-put-vs-post/

namespace DatabaseBackend.Controllers {
    [Route( "api/[controller]" )]
    [ApiController]
    public class UsersController : ControllerBase {

        private readonly ApiContext Db;

        public UsersController( ApiContext context ) {
            Db = context;
        }

        // GET: api/users
        [HttpGet( "" )]
        public async Task<IEnumerable<User>> GetUser() {
            return await Db.Users.ToListAsync();
        }

        // POST: api/users/
        [HttpPost( "" )]
        public async Task<User> CreateUser( string firstName, string lastName, string emailAddress, string password ) {

            emailAddress = emailAddress.ToLower();

            User user = new User(){
                FirstName       = firstName,
                LastName        = lastName,
                Email           = emailAddress,
                SecurityLevel   = SecurityLevel.User,
            };

            PasswordSecurity.SetPassword( password, user );

            Db.Users.Add( user );
            await Db.SaveChangesAsync();

            return user;
        }

        // GET: api/users/5
        [HttpGet( "{id}" )]
        public async Task<User> GetUser( int id ) {
            var user = await Db.Users.FindAsync(id);

            if ( user == null ) {

                return null;
            }

            return user;
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<User> UpdateUser( int id, User user ) {
            
            User dbuser = await Db.Users.FindAsync( id );
            if ( dbuser == null ) {
                return null;
            }

            user.ID = id;
            Db.Users.Update( user );
            await Db.SaveChangesAsync();

            return user;
        }

        // DELETE: api/users/5
        [HttpDelete( "{id}" )]
        public async Task<User> DeleteUser( int id ) {

            var user = await Db.Users.FindAsync(id);
            if ( user == null ) {
                return null;
            }

            Db.Users.Remove( user );
            await Db.SaveChangesAsync();

            return user;
        }

        // POST: api/users/login
        [HttpPost( "login") ]
        public async Task<string> LoginUser( string emailAddress, string password ) {

            emailAddress = emailAddress.ToLower();

            var user = await Db.Users.Where( x => x.Email == emailAddress ).FirstOrDefaultAsync();
            if ( user == null ) {

                return null;
            }



            return null;
        }
    }
}
