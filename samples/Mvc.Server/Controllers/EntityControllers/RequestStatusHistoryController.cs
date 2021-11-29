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
    public class RequestStatusHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestStatusHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestStatusHistory>>> Get()
        {
            return await _context.RequestStatusStories.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(RequestStatusHistory @RequestStatusHistory)
        {
            _context.Entry(@RequestStatusHistory).State = EntityState.Modified;

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
        public async Task<ActionResult<RequestStatusHistory>> Create(RequestStatusHistory @RequestStatusHistory)
        {
            @RequestStatusHistory.Id = Guid.NewGuid();
            _context.RequestStatusStories.Add(@RequestStatusHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestStatusHistory", new { id = @RequestStatusHistory.Id }, @RequestStatusHistory);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @RequestStatusHistory = await _context.RequestStatusStories.FindAsync(id);
                if (@RequestStatusHistory == null)
                {
                    return NotFound();
                }

                _context.RequestStatusStories.Remove(@RequestStatusHistory);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestStatusHistoryExists(Guid id)
        {
            return _context.RequestStatusStories.Any(e => e.Id == id);
        }
    }
}
