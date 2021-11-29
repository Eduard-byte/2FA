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
    public class ProgramPointBranchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProgramPointBranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramPointBranch>>> Get()
        {
            return await _context.ProgramPointBranches.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(ProgramPointBranch @programPointBranch)
        {
            _context.Entry(@programPointBranch).State = EntityState.Modified;

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
        public async Task<ActionResult<ProgramPointBranch>> Create(ProgramPointBranch @programPointBranch)
        {
            @programPointBranch.Id = Guid.NewGuid();
            _context.ProgramPointBranches.Add(@programPointBranch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgramPointBranch", new { id = @programPointBranch.Id }, @programPointBranch);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @programPointBranch = await _context.ProgramPointBranches.FindAsync(id);
                if (@programPointBranch == null)
                {
                    return NotFound();
                }

                _context.ProgramPointBranches.Remove(@programPointBranch);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramPointBranchExists(Guid id)
        {
            return _context.ProgramPointBranches.Any(e => e.Id == id);
        }
    }
}
