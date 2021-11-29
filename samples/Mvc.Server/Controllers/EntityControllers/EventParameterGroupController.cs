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
    public class EventParameterGroupController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventParameterGroupController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventParameterGroup>>> Get()
        {
            return await _context.EventParameterGroups.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(EventParameterGroup @eventParameterGroup)
        {
            _context.Entry(@eventParameterGroup).State = EntityState.Modified;

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
        public async Task<ActionResult<EventParameterGroup>> Create(EventParameterGroup @eventParameterGroup)
        {
            @eventParameterGroup.Id = Guid.NewGuid();
            _context.EventParameterGroups.Add(@eventParameterGroup);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventParameterGroup", new { id = @eventParameterGroup.Id }, @eventParameterGroup);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @eventParameterGroup = await _context.EventParameterGroups.FindAsync(id);
                if (@eventParameterGroup == null)
                {
                    return NotFound();
                }

                _context.EventParameterGroups.Remove(@eventParameterGroup);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventParameterGroupExists(Guid id)
        {
            return _context.EventParameterGroups.Any(e => e.Id == id);
        }
    }
}
