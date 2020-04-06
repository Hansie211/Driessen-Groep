using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseBackend;
using SharedLibrary.Models;

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

        // GET: api/users/5
        [HttpGet( "{id}" )]
        public async Task<User> GetUser( int id ) {
            var user = await Db.Users.FindAsync(id);

            if ( user == null ) {

                return null;
            }

            return user;
        }

        // PUT: api/users/create/
        [HttpPut( "create/{firstname}/{lastname}" )]
        public async Task<User> CreateUser( string firstName, string lastName ) {

            User user = new User();
            user.FirstName  = firstName;
            user.LastName   = lastName;

            return await CreateUser( user );
        }

        // POST: api/users/
        [HttpPost( "create/" )]
        public async Task<User> CreateUser( User user ) {

            Db.Users.Add( user );
            await Db.SaveChangesAsync();

            return user;
        }

        [HttpPost("update/")]
        public async Task<User> UpdateUser( User user ) {

            User dbuser = await Db.Users.FindAsync( user.ID );
            if ( dbuser == null ) {
                return null;
            }

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
    }
}
