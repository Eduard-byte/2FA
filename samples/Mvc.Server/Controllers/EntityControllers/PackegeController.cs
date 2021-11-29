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
    public class PackageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PackageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Package>>> Get()
        {
            return await _context.Packages.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(Package @Package)
        {
            _context.Entry(@Package).State = EntityState.Modified;

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
        public async Task<ActionResult<Package>> Create(Package @Package)
        {
            @Package.Id = Guid.NewGuid();
            _context.Packages.Add(@Package);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPackage", new { id = @Package.Id }, @Package);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @Package = await _context.Packages.FindAsync(id);
                if (@Package == null)
                {
                    return NotFound();
                }

                _context.Packages.Remove(@Package);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // [HttpPost]
        // public ValueTask<Package> UserPackage(Guid userId)
        // {
        //     var packageIds = (from item in _context.UserPackages
        //                        where item.CreatedUserId == userId
        //                        select item.PackageId).ToList();
            
        //     var packageId = packageIds[0];

        //     var userPackage =  _context.Packages.FindAsync(packageId);
        //     return userPackage;
        // }

        private bool PackageExists(Guid id)
        {
            return _context.Packages.Any(e => e.Id == id);
        }
    }
}
