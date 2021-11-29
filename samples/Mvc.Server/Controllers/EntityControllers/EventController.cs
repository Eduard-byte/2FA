using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Server.Models;
using OpenIddict.Validation.AspNetCore;

namespace Mvc.Server.Controllers
{
    [Route("api/[controller]/{action=Get}")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            return await _context.Events.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(Event @event)
        {
            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Create(Event @event)
        {
            @event.Id = Guid.NewGuid();
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @event = await _context.Events.FindAsync(id);
                if (@event == null)
                {
                    return NotFound();
                }

                _context.Events.Remove(@event);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
