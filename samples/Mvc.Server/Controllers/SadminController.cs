using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Mvc.Server.Models;

namespace Mvc.Server.Controllers
{
    [Authorize("Admin")]
    public class SadminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SadminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetEmployees()
        //{
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);

        //    if (user.EmployeePersonalDataId == null)
        //        throw new UnexpectedException(new ErrorMessage("Не удалось найти данные о сотруднике", "Staff data can not be found", "تعذر العثور على معلومات حول الموظفين"));
            
        //    var employees = await _sadminService.GetEmployees(user.Id, user.EmployeePersonalDataId.Value);

        //    foreach (var employee in employees)
        //    {
        //        var employeeAccount = await _userManager.FindByNameAsync(employee.UserName);
        //        var roles = await _userManager.GetRolesAsync(employeeAccount);
        //        employee.Roles = roles.ToList();
        //        var claims = await _userManager.GetClaimsAsync(employeeAccount);
        //        employee.Claim = claims.Select(e => e.Type).FirstOrDefault();
        //    }

        //    return Ok(employees);
        //}

        public async Task<IActionResult> GetValuesAsync()
        {
            var result = await _userManager.AddToRolesAsync(new ApplicationUser(), new List<string>());
            return ViewBag();
        }
    }
}
