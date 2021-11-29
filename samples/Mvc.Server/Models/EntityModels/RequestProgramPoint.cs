using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class RequestProgramPoint
    {
        [Key]
        public Guid Id { get; set; }

        public Guid RequestId { get; set; }
        public Guid ProgramPointId { get; set; }

        public virtual Request Request { get; set; }
        public virtual ProgramPoint ProgramPoint { get; set; }
    }
}