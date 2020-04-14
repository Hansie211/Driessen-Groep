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
                return NotFound( "Event not found" );
            }

            query.Include( e => e.Ownerships ).ThenInclude( o => o.User ).Load();
            query.Include( e => e.Programs ).Load();
            query.Include( e => e.Reviews ).Load();
            query.Include( e => e.Speakers ).Load();

            return @event;
        }

        // Post: api/events
        [HttpPost( "" )]
        public async Task<ActionResult<Event>> CreateEvent( Event eventdata ) {

            // Only moderator or higher can create event
            if ( AuthorizedSecurityLevel < SecurityLevel.Moderator ) {

                return Forbidden();
            }

            User user = await Db.Users.FindAsync( AuthorizedID );
            if ( user == null ) {
                return NotFound( "User not found" );
            }

            eventdata.Ownerships.Clear();
            eventdata.Ownerships.Add( new EventOwnership() { Event = eventdata, User = user, OwnershipLevel = OwnershipLevel.Administrator } );

            Db.Events.Add( eventdata );
            await Db.SaveChangesAsync();

            return eventdata;
        }

        // PUT: api/events/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Event>> UpdateEvent( int id, Event eventdata ) { 

            Event @event = await Db.Events.FindAsync( id );
            if ( @event == null ) {
                return NotFound( "Event not found" );
            }

            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Moderator ) {

                return Forbidden();
            }

            @event.CopyFromRequest( eventdata );
            Db.Events.Update( @event );
            await Db.SaveChangesAsync();

            return eventdata;
        }

        // DELETE: api/events/5
        [HttpDelete( "{id}" )]
        public async Task<ActionResult<Event>> DeleteEvent( int id ) {

            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Administrator ) {

                return Forbidden();
            }

            var @event = await Db.Events.FindAsync(id);
            if ( @event == null ) {
                return NotFound( "Event not found" );
            }

            Db.Events.Remove( @event );
            await Db.SaveChangesAsync();

            return @event;
        }

        // POST: api/events/5/ownership/2
        [HttpPost( "{id}/ownership/{userid}" )]
        public async Task<ActionResult> CreateOwner( int id, int userid, [FromBody]OwnershipLevel ownershipLevel ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Administrator ) {

                return Forbidden();
            }

            Event @event = await Db.Events.FindAsync( id );
            if ( @event == null ) {
                return NotFound( "Event not found" );
            }

            User user = await Db.Users.FindAsync( userid );
            if ( user == null ) {
                return NotFound( "User not found" );
            }

            EventOwnership ownership = await Db.EventOwnerships.FirstOrDefaultAsync( o => ( o.Event.ID == id ) && ( o.User.ID == userid ) );

            // Already set?
            if ( ownership?.OwnershipLevel == ownershipLevel ) {

                return Ok(); // done
            }

            if ( ownership != null ) {

                ownership.OwnershipLevel = ownershipLevel;
                Db.EventOwnerships.Update( ownership );
            } else {

                Db.EventOwnerships.Add( new EventOwnership() { Event = @event, User = user, OwnershipLevel = ownershipLevel } );
            }

            await Db.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/events/5/ownership/2
        [HttpDelete( "{id}/ownership/{userid}" )]
        public async Task<ActionResult> DeleteOwner( int id, int userid ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Administrator ) {

                if ( userid != AuthorizedID ) {

                    return Forbidden();
                }
            }

            // Does this ownership exist?
            EventOwnership ownership = await Db.EventOwnerships.FirstOrDefaultAsync( o => ( o.Event.ID == id ) && ( o.User.ID == userid ) );
            if ( ownership == null ) { 

                return NotFound( "Ownership not found");
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
        public async Task<ActionResult<Speaker>> CreateSpeaker( int id, [FromBody]Speaker speakerdata ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Moderator ) {

                return Forbidden();
            }

            Event @event = await Db.Events.FindAsync( id );            
            if ( @event == null ) {
                return NotFound( "Event not found" );
            }

            @event.Speakers.Add( speakerdata );
            await Db.SaveChangesAsync();

            return speakerdata;
        }

        // PUT: api/events/5/speakers/2
        [HttpPut( "{id}/speakers/{speakerid}" )]
        public async Task<ActionResult<Speaker>> UpdateSpeaker( int id, int speakerid, [FromBody]Speaker speakerdata ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Moderator ) {

                return Forbidden();
            }

            Speaker speaker = await Db.Speakers.Where( s => s.ID == speakerid ).FirstOrDefaultAsync();
            if ( speaker == null ) {
                return NotFound( "Speaker not found" );
            }

            if ( speaker.EventID != id ) {
                return BadRequest();
            }

            speaker.CopyFromRequest( speakerdata );
            Db.Speakers.Update( speaker );

            await Db.SaveChangesAsync();

            return speaker;
        }

        // DELETE: api/events/5/speakers/2
        [HttpDelete( "{id}/speakers/{speakerid}" )]
        public async Task<ActionResult<Speaker>> DeleteSpeaker( int id, int speakerid ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Moderator ) {

                return Forbidden();
            }

            Speaker speaker = await Db.Speakers.FindAsync( speakerid );            
            if ( speaker == null ) {
                return NotFound( "Speaker not found" );
            }

            if ( speaker.EventID != id ) {

                return BadRequest();
            }

            Db.Speakers.Remove( speaker );
            await Db.SaveChangesAsync();

            return speaker;
        }

        // POST: api/events/5/programs/
        [HttpPost( "{id}/programs" )]
        public async Task<ActionResult<EventProgram>> CreateProgram( int id, [FromBody]EventProgram programdata ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Moderator ) {

                return Forbidden();
            }

            Event @event = await Db.Events.FindAsync( id );
            if ( @event == null ) {
                return NotFound( "Event not found" );
            }

            @event.Programs.Add( programdata );
            await Db.SaveChangesAsync();

            return programdata;
        }

        // PUT: api/events/5/programs/2
        [HttpPut( "{id}/programs/{programid}" )]
        public async Task<ActionResult<EventProgram>> UpdateProgram( int id, int programid, [FromBody]EventProgram programdata ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Moderator ) {

                return Forbidden();
            }

            EventProgram program = await Db.EventPrograms.FindAsync( programid );
            if ( program == null ) {
                return NotFound( "Program not found" );
            }

            if ( program.EventID != id ) {

                return BadRequest();
            }

            program.CopyFromRequest( programdata );
            Db.EventPrograms.Update( program );
            await Db.SaveChangesAsync();

            return program;
        }

        // DELETE: api/events/5/programs/2
        [HttpDelete( "{id}/programs/{programid}" )]
        public async Task<ActionResult<EventProgram>> DeleteProgram( int id, int programid ) {

            // Is the requester authorized?
            if ( GetAuthorizedOwnershipLevel( id ) < OwnershipLevel.Moderator ) {

                return Forbidden();
            }

            EventProgram program = await Db.EventPrograms.FindAsync( programid );
            if ( program == null ) {
                return NotFound( "Program not found" );
            }

            if ( program.EventID != id ) {

                return BadRequest();
            }

            Db.EventPrograms.Remove( program );
            await Db.SaveChangesAsync();

            return program;
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
