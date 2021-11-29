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
    public class ParameterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParameterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parameter>>> Get()
        {
            return await _context.Parameters.ToListAsync();
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(Parameter @parameter)
        {
            // Проверка на наличие номера данного типа параметра в перечислении
            bool isParameterType = Enum.IsDefined(typeof(ParameterType), @parameter.Type);
            if(!isParameterType) return NotFound();

            _context.Entry(@parameter).State = EntityState.Modified;

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
        public async Task<ActionResult<Parameter>> Create(Parameter @parameter)
        {
            // Проверка на наличие номера данного типа параметра
            bool isParameterType = Enum.IsDefined(typeof(ParameterType), @parameter.Type);
            if(!isParameterType) return NotFound();
            
            @parameter.Id = Guid.NewGuid();
            _context.Parameters.Add(@parameter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParameter", new { id = @parameter.Id }, @parameter);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @parameter = await _context.Parameters.FindAsync(id);
                if (@parameter == null)
                {
                    return NotFound();
                }

                _context.Parameters.Remove(@parameter);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParameterExists(Guid id)
        {
            return _context.Parameters.Any(e => e.Id == id);
        }
    }
}
