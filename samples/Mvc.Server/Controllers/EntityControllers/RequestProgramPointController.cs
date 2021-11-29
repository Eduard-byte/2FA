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
    public class RequestProgramPointController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RequestProgramPointController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestProgramPoint>>> Get()
        {
            return await _context.RequestProgramPoints.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(RequestProgramPoint @requestProgramPoint)
        {
            _context.Entry(@requestProgramPoint).State = EntityState.Modified;

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
        public async Task<ActionResult<RequestProgramPoint>> Create(RequestProgramPoint @requestProgramPoint)
        {
            @requestProgramPoint.Id = Guid.NewGuid();
            _context.RequestProgramPoints.Add(@requestProgramPoint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestProgramPoint", new { id = @requestProgramPoint.Id }, @requestProgramPoint);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @requestProgramPoint = await _context.RequestProgramPoints.FindAsync(id);
                if (@requestProgramPoint == null)
                {
                    return NotFound();
                }

                _context.RequestProgramPoints.Remove(@requestProgramPoint);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestProgramPointExists(Guid id)
        {
            return _context.RequestProgramPoints.Any(e => e.Id == id);
        }
    }
}
