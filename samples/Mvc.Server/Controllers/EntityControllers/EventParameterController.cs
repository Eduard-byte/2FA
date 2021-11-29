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
    public class EventParameterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventParameterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventParameter>>> Get()
        {
            return await _context.EventParameters.ToListAsync();
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(EventParameter @eventParameter)
        {
            _context.Entry(@eventParameter).State = EntityState.Modified;

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
        public async Task<ActionResult<EventParameter>> Create(EventParameter @eventParameter)
        {
            @eventParameter.Id = Guid.NewGuid();
            _context.EventParameters.Add(@eventParameter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventParameter", new { id = @eventParameter.Id }, @eventParameter);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @eventParameter = await _context.EventParameters.FindAsync(id);
                if (@eventParameter == null)
                {
                    return NotFound();
                }

                _context.EventParameters.Remove(@eventParameter);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventParameterExists(Guid id)
        {
            return _context.EventParameters.Any(e => e.Id == id);
        }
    }
}
