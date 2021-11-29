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
    public class RequestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> Get()
        {
            return await _context.Requests.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(Request @request)
        {
            _context.Entry(@request).State = EntityState.Modified;

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
        public async Task<ActionResult<Request>> Create(Request @request)
        {
            @request.Id = Guid.NewGuid();
            _context.Requests.Add(@request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = @request.Id }, @request);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @request = await _context.Requests.FindAsync(id);
                if (@request == null)
                {
                    return NotFound();
                }

                _context.Requests.Remove(@request);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApplicationExists(Guid id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
