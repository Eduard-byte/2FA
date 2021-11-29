using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class PackageProgramPoint
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PackageId { get; set; }
        public Guid ProgramPointId { get; set; }

        public virtual Package Package { get; set; }
        public virtual ProgramPoint ProgramPoint { get; set; }
    }
}