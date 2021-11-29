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
    public class EventProgramController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventProgramController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventProgram>>> Get()
        {
            return await _context.EventPrograms.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(EventProgram @eventProgram)
        {
            _context.Entry(@eventProgram).State = EntityState.Modified;

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
        public async Task<ActionResult<EventProgram>> Create(EventProgram @eventProgram)
        {
            @eventProgram.Id = Guid.NewGuid();
            _context.EventPrograms.Add(@eventProgram);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventProgram", new { id = @eventProgram.Id }, @eventProgram);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @eventProgram = await _context.EventPrograms.FindAsync(id);
                if (@eventProgram == null)
                {
                    return NotFound();
                }

                _context.EventPrograms.Remove(@eventProgram);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventProgramExists(Guid id)
        {
            return _context.EventPrograms.Any(e => e.Id == id);
        }
    }
}
