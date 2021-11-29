using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Mvc.Server.Models;
using Mvc.Server.Services;

namespace Mvc.Server.Controllers.EntityControllers
{
    [AllowAnonymous]
    public class RegisterConfirmationController : Controller
    {
        private readonly UserManager<ApplicationUser> _manager;
        private readonly IEmailSender _emailSender;

        public RegisterConfirmationController(UserManager<ApplicationUser> manager, IEmailSender emailSender)
        {
            _manager = manager;
            _emailSender = emailSender;
        }

        public string Email { get; set; }
        public bool DisplayConfirmAccLink { get; set; }
        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> onGetAsync(string email, string urlReturn)
        {
            if (email == null)
            {
                return Redirect("Index");
            }

            var user = await _manager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("Такого пользователя нет");
            }

            Email = email;

            DisplayConfirmAccLink = false;

            if (DisplayConfirmAccLink)
            {
                var userId = await _manager.GetUserIdAsync(user);

                var code = await _manager.GenerateEmailConfirmationTokenAsync(user);

                EmailConfirmationUrl = Url.RouteUrl(
                    "/Account/ConfirmEmail",
                    new {area = "Identity", userId, code},
                    Request.Scheme);
            }

            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
