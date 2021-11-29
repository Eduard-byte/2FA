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
    public class PhysicalZoneProgramPointController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhysicalZoneProgramPointController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhysicalZoneProgramPoint>>> Get()
        {
            return await _context.PhysicalZoneProgramPoints.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(PhysicalZoneProgramPoint @physicalZoneProgramPoint)
        {
            _context.Entry(@physicalZoneProgramPoint).State = EntityState.Modified;

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
        public async Task<ActionResult<PhysicalZoneProgramPoint>> Create(PhysicalZoneProgramPoint @physicalZoneProgramPoint)
        {
            @physicalZoneProgramPoint.Id = Guid.NewGuid();
            _context.PhysicalZoneProgramPoints.Add(@physicalZoneProgramPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhysicalZoneProgramPoint", new { id = @physicalZoneProgramPoint.Id }, @physicalZoneProgramPoint);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @physicalZoneProgramPoint = await _context.PhysicalZoneProgramPoints.FindAsync(id);
                if (@physicalZoneProgramPoint == null)
                {
                    return NotFound();
                }

                _context.PhysicalZoneProgramPoints.Remove(@physicalZoneProgramPoint);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhysicalZoneProgramPointExists(Guid id)
        {
            return _context.PhysicalZoneProgramPoints.Any(e => e.Id == id);
        }
    }
}
