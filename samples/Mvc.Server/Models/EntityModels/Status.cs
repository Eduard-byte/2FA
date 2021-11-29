using System;
using System.ComponentModel.DataAnnotations;
namespace Mvc.Server.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool Inactive { get; set; }
    }
}