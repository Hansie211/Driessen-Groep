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
        private readonly ApiContext _context;

        public EventsController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/events
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/events/myevent
        [HttpPut("{name}")]
        // PUT: api/events
        [HttpPost("")]
        public async Task<IActionResult> CreateEvent( string name )
        {

            Event @event = new Event() {

                Name = name,
            };

            _context.Events.Add( @event );
            await _context.SaveChangesAsync();

            return CreatedAtAction( "GetEvent", new { id = @event.ID }, @event );
        }

        // DELETE: api/events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }
    }
}
