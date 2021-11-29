using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class LogicalZonePackage
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ZoneId { get; set; }
        public Guid PackageId { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual Package Package { get; set; }
    }
}