using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatabaseBackend;
using SharedLibrary.Models;

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

        // PUT: api/events/create/
        [HttpPut("create/{name}")]
        public async Task<Event> CreateEvent( string name, string description )
        {

            description = "kort";

            Event @event = new Event() {

                Name = name,
                Description = description,
            };

            return await CreateEvent( @event );
        }

        // PUT: api/events/create
        [HttpPost( "create/" )]
        public async Task<Event> CreateEvent( Event @event ) {

            Db.Events.Add( @event );
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
