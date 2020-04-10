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
using Microsoft.AspNetCore.Authorization;

// https://restfulapi.net/rest-put-vs-post/

namespace DatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : BaseController {
        
        public EventsController( ApiContext context ) : base( context ) {
        }

        // GET: api/events
        [HttpGet("")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await Db.Events.ToListAsync();
        }

        // GET: api/events/5
        [HttpGet( "{id}" )]
        [AllowAnonymous]
        public async Task<ActionResult<Event>> GetEvent( int id ) {

            IQueryable<Event> query = Db.Events.Where( @e => @e.ID == id );
            Event @event = await query.FirstOrDefaultAsync();
            if ( @event == null ) {
                return NotFound();
            }

            query.Include( e => e.Ownerships ).ThenInclude( o => o.User ).Load();
            query.Include( e => e.Programs ).Load();
            query.Include( e => e.Reviews ).Load();
            query.Include( e => e.Speakers ).Load();

            return @event;
        }

        // Post: api/events
        [HttpPost( "" )]
        public async Task<ActionResult<Event>> CreateEvent( Event @event ) {

            User user = await Db.Users.FindAsync( AuthorizedID );
            if ( user == null ) {
                return BadRequest();
            }

            @event.Ownerships.Clear();
            @event.Ownerships.Add( new EventOwnership() { Event = @event, User = user, OwnershipLevel = OwnershipLevel.Administrator } );

            Db.Events.Add( @event );
            await Db.SaveChangesAsync();

            return @event;
        }

        // PUT: api/events/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Event>> UpdateEvent( int id, Event @event ) { 

            Event dbevent = await Db.Events.FindAsync( id );
            if ( dbevent == null ) {
                return NotFound();
            }

            if ( AuthorizedSecurityLevel < SecurityLevel.Administrator ) { 

                EventOwnership ownership = await Db.EventOwnerships.FirstOrDefaultAsync( o => (o.Event.ID == id) && (o.User.ID == AuthorizedID ) );
                if ( ownership == null ) {

                    return ValidationProblem();
                }
            }

            dbevent.CopyFromRequest( @event );
            Db.Events.Update( dbevent );
            await Db.SaveChangesAsync();

            return @event;
        }

        // DELETE: api/events/5
        [HttpDelete( "{id}" )]
        public async Task<ActionResult<Event>> DeleteEvent( int id ) {
            
            var @event = await Db.Events.FindAsync(id);
            if ( @event == null ) {
                return NotFound();
            }

            if ( AuthorizedSecurityLevel < SecurityLevel.Administrator ) {

                EventOwnership ownership = await Db.EventOwnerships.FirstOrDefaultAsync( o => (o.Event.ID == id) && (o.User.ID == AuthorizedID ) );
                if ( ( ownership == null ) || ( ownership.OwnershipLevel < OwnershipLevel.Administrator ) ) {

                    return ValidationProblem();
                }
            }

            Db.Events.Remove( @event );
            await Db.SaveChangesAsync();

            return @event;
        }

        // POST: api/events/5/ownership/2
        [HttpPost( "{id}/ownership/{userid}" )]
        public async Task<ActionResult> CreateOwner( int id, int userid, [FromBody]OwnershipLevel ownershipLevel ) {

            Event @event = await Db.Events.FindAsync( id );
            if ( @event == null ) {
                return NotFound();
            }

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Administrator ) {

                return ValidationProblem();
            }

            User user = await Db.Users.FindAsync( userid );
            if ( user == null ) {
                return NotFound();
            }

            EventOwnership ownership = await Db.EventOwnerships.FirstOrDefaultAsync( o => ( o.Event.ID == id ) && ( o.User.ID == userid ) );
            if ( ownership == null ) {

                Db.EventOwnerships.Add( new EventOwnership() { Event = @event, User = user, OwnershipLevel = ownershipLevel } );
            } else {

                if ( ownership.OwnershipLevel == ownershipLevel ) {
                    return Ok(); // Done
                }

                ownership.OwnershipLevel = ownershipLevel;

                Db.EventOwnerships.Attach( ownership );
                Db.Entry( ownership ).Property( o => o.OwnershipLevel ).IsModified = true;
            }

            await Db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/events/5/ownership/2
        [HttpDelete( "{id}/ownership/{userid}" )]
        public async Task<ActionResult> DeleteOwner( int id, int userid ) {

            EventOwnership ownership = await Db.EventOwnerships.FirstOrDefaultAsync( o => ( o.Event.ID == id ) && ( o.User.ID == userid ) );
            if ( ownership == null ) { 

                return NotFound();
            }

            // Is the requester authorized?
            if ( GetOwnershipLevel( id, AuthorizedID ) < OwnershipLevel.Administrator ) {

                if ( userid != AuthorizedID ) {

                    return ValidationProblem();
                }
            }

            // Any admins left?
            if (( ownership.OwnershipLevel == OwnershipLevel.Administrator ) && (Db.EventOwnerships.Count( o => (o.Event.ID == id) && (o.OwnershipLevel == OwnershipLevel.Administrator) ) == 1 ) ){

                return BadRequest( "Cannot remove last administrator from an event." );
            }

            Db.EventOwnerships.Remove( ownership );
            await Db.SaveChangesAsync();

            return Ok();
        }

        // POST: api/events/5/speakers/
        [HttpPost( "{id}/speakers")]
        public async Task<ActionResult<Speaker>> CreateSpeaker( int id, [FromBody]Speaker speaker ) {

            Event @event = await Db.Events.FindAsync( id );
            
            if ( @event == null ) {
                return NotFound();
            }

            @event.Speakers.Add( speaker );
            await Db.SaveChangesAsync();

            return speaker;
        }

        // PUT: api/events/5/speakers/2
        [HttpPut( "{id}/speakers/{speakerid}" )]
        public async Task<ActionResult<Speaker>> UpdateSpeaker( int id, int speakerid, [FromBody]Speaker speaker ) {

            IQueryable<Speaker> query = Db.Speakers.Where( s => s.ID == speakerid );
            Speaker dbspeaker = await query.FirstOrDefaultAsync();
            if ( dbspeaker == null ) {
                return NotFound();
            }

            query.Include( s => s.Event ).Load();
            if ( dbspeaker.Event.ID != id ) {
                return BadRequest();
            }

            dbspeaker.CopyFromRequest( speaker );
            Db.Speakers.Update( dbspeaker );
            await Db.SaveChangesAsync();

            return dbspeaker;
        }

        // DELETE: api/events/5/speakers/2
        [HttpDelete( "{id}/speakers/{speakerid}" )]
        public async Task<ActionResult<Speaker>> DeleteSpeaker( int id, int speakerid ) {
            
            Speaker dbspeaker = await Db.Speakers.FindAsync( speakerid );
            
            if ( dbspeaker == null ) {
                return NotFound();
            }

            Db.Speakers.Include( o => o.Event ).Load();
            if ( dbspeaker.Event.ID != id ) {

                return BadRequest();
            }

            Db.Speakers.Remove( dbspeaker );
            await Db.SaveChangesAsync();

            return dbspeaker;
        }

        // POST: api/events/5/programs/
        [HttpPost( "{id}/programs" )]
        public async Task<ActionResult<EventProgram>> CreateProgram( int id, [FromBody]EventProgram program ) {

            Event @event = await Db.Events.FindAsync( id );

            if ( @event == null ) {
                return NotFound();
            }

            @event.Programs.Add( program );
            await Db.SaveChangesAsync();

            return program;
        }

        // PUT: api/events/5/programs/2
        [HttpPut( "{id}/programs/{programid}" )]
        public async Task<ActionResult<EventProgram>> UpdateProgram( int id, int programid, [FromBody]EventProgram program ) {

            EventProgram dbprogram = await Db.EventPrograms.FindAsync( programid );
            if ( dbprogram == null ) {
                return NotFound();
            }

            Db.EventPrograms.Include( o => o.Event ).Load();
            if ( dbprogram.Event.ID != id ) {

                return BadRequest();
            }

            dbprogram.CopyFromRequest( program );
            Db.EventPrograms.Update( dbprogram );
            await Db.SaveChangesAsync();

            return dbprogram;
        }

        // DELETE: api/events/5/programs/2
        [HttpDelete( "{id}/programs/{programid}" )]
        public async Task<ActionResult<EventProgram>> DeleteProgram( int id, int programid ) {

            EventProgram dbprogram = await Db.EventPrograms.FindAsync( programid );
            if ( dbprogram == null ) {
                return NotFound();
            }

            Db.EventPrograms.Include( o => o.Event ).Load();
            if ( dbprogram.Event.ID != id ) {

                return BadRequest();
            }

            Db.EventPrograms.Remove( dbprogram );
            await Db.SaveChangesAsync();

            return dbprogram;
        }

        private OwnershipLevel GetAuthorizedOwnershipLevel( int eventid ) {

            if ( AuthorizedSecurityLevel >= SecurityLevel.Administrator ) {
                return OwnershipLevel.Administrator;
            }

            EventOwnership ownership = Db.EventOwnerships.FirstOrDefault( o => ( o.Event.ID == eventid ) && ( o.User.ID == AuthorizedID ) );
            if ( ownership == null ) {
                return OwnershipLevel.None;
            }

            return ownership.OwnershipLevel;
        }

    }
}
