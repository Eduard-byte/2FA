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
    public class BranchController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BranchController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Branch>>> Get()
        {
            return await _context.Branches.ToListAsync();
        }

        [HttpPost]
        // [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update(Branch @branch)
        {
            _context.Entry(@branch).State = EntityState.Modified;

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
        public async Task<ActionResult<Branch>> Create(Branch @branch)
        {
            @branch.Id = Guid.NewGuid();
            _context.Branches.Add(@branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBranch", new { id = @branch.Id }, @branch);
        }

       [HttpPost]
        public async Task<IActionResult> Delete(DeleteModel model)
        {
            foreach (var id in model.Ids)
            {
                var @branch = await _context.Branches.FindAsync(id);
                if (@branch == null)
                {
                    return NotFound();
                }

                _context.Branches.Remove(@branch);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BranchExists(Guid id)
        {
            return _context.Branches.Any(e => e.Id == id);
        }
    }
}
