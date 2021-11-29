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
    public class LogicalZonePackageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogicalZonePackageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogicalZonePackage>>> Get()
        {
            return await _context.LogicalZonePackages.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(LogicalZonePackage @logicalZonePackage)
        {
            _context.Entry(@logicalZonePackage).State = EntityState.Modified;

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
        public async Task<ActionResult<LogicalZonePackage>> Create(LogicalZonePackage @logicalZonePackage)
        {
            @logicalZonePackage.Id = Guid.NewGuid();
            _context.LogicalZonePackages.Add(@logicalZonePackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogicalZonePackage", new { id = @logicalZonePackage.Id }, @logicalZonePackage);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @logicalZonePackage = await _context.LogicalZonePackages.FindAsync(id);
                if (@logicalZonePackage == null)
                {
                    return NotFound();
                }

                _context.LogicalZonePackages.Remove(@logicalZonePackage);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogicalZonePackageExists(Guid id)
        {
            return _context.LogicalZonePackages.Any(e => e.Id == id);
        }
    }
}
