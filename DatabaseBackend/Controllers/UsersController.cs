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
        private readonly ApiContext _context;

        public UsersController( ApiContext context ) {
            _context = context;
        }

        // GET: api/users
        [HttpGet( "" )]
        public async Task<ActionResult<IEnumerable<User>>> GetUser() {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet( "{id}" )]
        public async Task<ActionResult<User>> GetUser( int id ) {
            var user = await _context.Users.FindAsync(id);

            if ( user == null ) {
                return NotFound();
            }

            return user;
        }

        // PUT: api/users/
        [HttpPut( "{firstname}/{lastname}" )]
        // POST: api/users/
        [HttpPost( "" )]
        public async Task<IActionResult> CreateUser( string firstName, string lastName ) {

            User user = new User();
            user.FirstName  = firstName;
            user.LastName   = lastName;

            _context.Users.Add( user );
            await _context.SaveChangesAsync();

            return CreatedAtAction( "GetUser", new { id = user.ID }, user );
        }

        // DELETE: api/users/5
        [HttpDelete( "{id}" )]
        public async Task<ActionResult<User>> DeleteUser( int id ) {
            var user = await _context.Users.FindAsync(id);
            if ( user == null ) {
                return NotFound();
            }

            _context.Users.Remove( user );
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
