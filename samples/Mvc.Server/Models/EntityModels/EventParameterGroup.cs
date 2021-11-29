using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class EventParameterGroup
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}