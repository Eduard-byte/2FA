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
    public class ProgramPointController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProgramPointController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramPoint>>> Get()
        {
            return await _context.ProgramPoints.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(ProgramPoint @programPoint)
        {
            _context.Entry(@programPoint).State = EntityState.Modified;

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
        public async Task<ActionResult<ProgramPoint>> Create(ProgramPoint @programPoint)
        {
            @programPoint.Id = Guid.NewGuid();
            _context.ProgramPoints.Add(@programPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgramPoint", new { id = @programPoint.Id }, @programPoint);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @programPoint = await _context.ProgramPoints.FindAsync(id);
                if (@programPoint == null)
                {
                    return NotFound();
                }

                _context.ProgramPoints.Remove(@programPoint);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramPointExists(Guid id)
        {
            return _context.ProgramPoints.Any(e => e.Id == id);
        }
    }
}
