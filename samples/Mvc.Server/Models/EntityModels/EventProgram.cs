using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class EventProgram
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string NameArab { get; set; }
        public string NameEnglish { get; set; }
        public string Description { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}