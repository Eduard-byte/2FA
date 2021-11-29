using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class Request
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid PackageId { get; set; }
        public string CreatedUserId { get; set; }

        public virtual Package Package { get; set; }
        public virtual ApplicationUser CreatedUser { get; set; }
    }
}