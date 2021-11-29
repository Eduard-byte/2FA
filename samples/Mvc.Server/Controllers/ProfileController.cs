using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Server.Models;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Mvc.Server.Controllers
{
    [Route("api")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }


        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("recording")]
        public async Task<IActionResult> Recording(string username)
        {

            var user = await _userManager.GetUserAsync(User);
            var res = check(user);
            if (res != null)
                return res;

            user.UserName = username;
            await _userManager.UpdateAsync(user);

            // Сообщение передаваемое другому сервису
            return Content($"{user.UserName} Запись {username}!");
        }

        private IActionResult check(ApplicationUser user)
        {
            // Пользователь должен иметь Scope demo_api
            if (!User.HasScope("demo_api"))
            {
                return Forbid(
                    authenticationSchemes: OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictValidationAspNetCoreConstants.Properties.Scope] = "demo_api",
                        [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InsufficientScope,
                        [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                            "The 'demo_api' scope is required to perform this action."
                    }));
            }

            // Проверка на существование пользователя
            
            if (user is null)
            {
                return Challenge(
                    authenticationSchemes: OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                        [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                            "The specified access token is bound to an account that no longer exists."
                    }));
            }
            return null;
        }

        [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("recovery")]
        public async Task<IActionResult> Return()
        {
            if (!User.HasScope("demo_api"))
            {
                // Пользователь должен иметь Scope demo_api
                return Forbid(
                    authenticationSchemes: OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictValidationAspNetCoreConstants.Properties.Scope] = "demo_api",
                        [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InsufficientScope,
                        [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                            "The 'demo_api' scope is required to perform this action."
                    }));
            }

            // Проверка на существование пользователя
            var user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return Challenge(
                    authenticationSchemes: OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string>
                    {
                        [OpenIddictValidationAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                        [OpenIddictValidationAspNetCoreConstants.Properties.ErrorDescription] =
                            "The specified access token is bound to an account that no longer exists."
                    }));
            }

            // Сообщение предоставляемое другому сервису
            return Content($"{user.UserName} Возврат!");
        }
    }
}

