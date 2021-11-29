using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Mvc.Server.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser()
        //{
        //    ParameterValues = new HashSet<ParameterValue>();
        //}

        //public virtual ICollection<ParameterValue> ParameterValues { get; set; }
    }
}
