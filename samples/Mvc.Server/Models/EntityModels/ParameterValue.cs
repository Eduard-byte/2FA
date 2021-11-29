using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mvc.Server.Models
{
  
    public class ParameterValue
    {
        [Key]
        public Guid Id { get; set; }

        public string Value { get; set; }
        public bool Hidden { get; set; }

        public Guid ParameterId { get; set; }
        public Guid EventId { get; set; }

        public virtual Parameter Parameter { get; set; }
        public virtual Event Event { get; set; }
    }
}
