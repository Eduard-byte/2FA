using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class PhysicalZoneProgramPoint
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ZoneId { get; set; }
        public Guid ProgramPointId { get; set; }

        public virtual Zone Zone { get; set; }
        public virtual ProgramPoint ProgramPoint { get; set; }
    }
}