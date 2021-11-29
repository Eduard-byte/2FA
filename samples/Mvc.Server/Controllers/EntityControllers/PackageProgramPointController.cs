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
    public class PackageProgramPointController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PackageProgramPointController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PackageProgramPoint>>> Get()
        {
            return await _context.PackageProgramPoints.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(PackageProgramPoint @packageProgramPoint)
        {
            _context.Entry(@packageProgramPoint).State = EntityState.Modified;

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
        public async Task<ActionResult<PackageProgramPoint>> Create(PackageProgramPoint @packageProgramPoint)
        {
            @packageProgramPoint.Id = Guid.NewGuid();
            _context.PackageProgramPoints.Add(@packageProgramPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackageProgramPoint", new { id = @packageProgramPoint.Id }, @packageProgramPoint);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @packageProgramPoint = await _context.PackageProgramPoints.FindAsync(id);
                if (@packageProgramPoint == null)
                {
                    return NotFound();
                }

                _context.PackageProgramPoints.Remove(@packageProgramPoint);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PackageProgramPointExists(Guid id)
        {
            return _context.PackageProgramPoints.Any(e => e.Id == id);
        }
    }
}
