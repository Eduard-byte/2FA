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
    public class ParameterValueController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParameterValueController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParameterValue>>> Get()
        {
            return await _context.ParameterValues.ToListAsync();
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(ParameterValue @parameterValue)
        {
            _context.Entry(@parameterValue).State = EntityState.Modified;

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
        public async Task<ActionResult<ParameterValue>> Create(ParameterValue @parameterValue)
        {
            @parameterValue.Id = Guid.NewGuid();
            _context.ParameterValues.Add(@parameterValue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParameterValue", new { id = @parameterValue.Id }, @parameterValue);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @parameterValue = await _context.ParameterValues.FindAsync(id);
                if (@parameterValue == null)
                {
                    return NotFound();
                }

                _context.ParameterValues.Remove(@parameterValue);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParameterValueExists(Guid id)
        {
            return _context.ParameterValues.Any(e => e.Id == id);
        }
    }
}
