using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseBackend;
using SharedLibrary.Models;

// https://restfulapi.net/rest-put-vs-post/

namespace DatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApiContext Db;

        public EventsController(ApiContext context)
        {
            Db = context;
        }

        // GET: api/events
        [HttpGet("")]
        public async Task<IEnumerable<Event>> GetEvents()
        {
            return await Db.Events.ToListAsync();
        }

        // Post: api/events
        [HttpPost( "" )]
        public async Task<Event> CreateEvent( Event @event ) {

            Db.Events.Add( @event );
            await Db.SaveChangesAsync();

            return @event;
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<Event> GetEvent(int id)
        {
            var @event = await Db.Events.FindAsync(id);

            if (@event == null)
            {
                return null;
            }

            return @event;
        }

        // PUT: api/events/5
        [HttpPut("{id}")]
        public async Task<Event> UpdateEvent( int id, Event @event ) { 

            Event dbevent = await Db.Events.FindAsync( id );
            if ( dbevent == null ) {
                return null;
            }

            dbevent.ID = id;
            Db.Events.Update( @event );
            await Db.SaveChangesAsync();

            return @event;
        }

        // DELETE: api/events/5
        [HttpDelete("{id}")]
        public async Task<Event> DeleteEvent(int id)
        {
            var @event = await Db.Events.FindAsync(id);
            if (@event == null)
            {
                return null;
            }

            Db.Events.Remove(@event);
            await Db.SaveChangesAsync();

            return @event;
        }
    }
}
