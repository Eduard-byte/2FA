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
    public class ZoneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ZoneController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zone>>> Get()
        {
            return await _context.Zones.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(Zone @zone)
        {
            _context.Entry(@zone).State = EntityState.Modified;

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
        public async Task<ActionResult<Zone>> Create(Zone @zone)
        {
            @zone.Id = Guid.NewGuid();
            _context.Zones.Add(@zone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZone", new { id = @zone.Id }, @zone);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @zone = await _context.Zones.FindAsync(id);
                if (@zone == null)
                {
                    return NotFound();
                }

                _context.Zones.Remove(@zone);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZoneExists(Guid id)
        {
            return _context.Zones.Any(e => e.Id == id);
        }
    }
}
