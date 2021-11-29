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
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StatusController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> Get()
        {
            return await _context.Statuses.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(Status @status)
        {
            _context.Entry(@status).State = EntityState.Modified;

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
        public async Task<ActionResult<Status>> Create(Status @status)
        {
            @status.Id = Guid.NewGuid();
            _context.Statuses.Add(@status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = @status.Id }, @status);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @status = await _context.Statuses.FindAsync(id);
                if (@status == null)
                {
                    return NotFound();
                }

                _context.Statuses.Remove(@status);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(Guid id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}
