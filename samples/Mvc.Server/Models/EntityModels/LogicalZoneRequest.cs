using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class LogicalZoneRequest
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ZoneId { get; set; }
        public Guid RequestId { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual Request Request { get; set; }
    }
}