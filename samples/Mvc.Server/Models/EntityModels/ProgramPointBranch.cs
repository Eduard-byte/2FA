using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class ProgramPointBranch
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid ProgramPointId { get; set; }
        public Guid BranchId { get; set; }

        public virtual ProgramPoint ProgramPoint { get; set; }
        public virtual Branch Branch { get; set; }
    }
}