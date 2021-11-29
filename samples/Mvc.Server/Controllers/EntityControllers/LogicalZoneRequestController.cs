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
    public class LogicalZoneRequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LogicalZoneRequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogicalZoneRequest>>> Get()
        {
            return await _context.LogicalZoneRequests.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(LogicalZoneRequest @logicalZoneRequest)
        {
            _context.Entry(@logicalZoneRequest).State = EntityState.Modified;

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
        public async Task<ActionResult<LogicalZoneRequest>> Create(LogicalZoneRequest @logicalZoneRequest)
        {
            @logicalZoneRequest.Id = Guid.NewGuid();
            _context.LogicalZoneRequests.Add(@logicalZoneRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogicalZoneRequest", new { id = @logicalZoneRequest.Id }, @logicalZoneRequest);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @logicalZoneRequest = await _context.LogicalZoneRequests.FindAsync(id);
                if (@logicalZoneRequest == null)
                {
                    return NotFound();
                }

                _context.LogicalZoneRequests.Remove(@logicalZoneRequest);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogicalZoneRequestExists(Guid id)
        {
            return _context.LogicalZoneRequests.Any(e => e.Id == id);
        }
    }
}
