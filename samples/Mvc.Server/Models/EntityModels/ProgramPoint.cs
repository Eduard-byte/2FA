using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class ProgramPoint
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string NameArab { get; set; }
        public string NameEnglish { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public Guid EventProgramId { get; set; }

        public virtual EventProgram EventProgram { get; set; }
    }
}