using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Mvc.Server.ViewModels.Account
{
    public class VerifyCodeViewModel
    {
        public string UserId { get; set; }

        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }
    }
}
