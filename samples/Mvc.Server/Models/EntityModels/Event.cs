using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Server.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string NameArab { get; set; }
        public string NameEnglish { get; set; }
    }
}
