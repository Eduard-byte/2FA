using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class Package
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string NameArab { get; set; }
        public string NameEnglish { get; set; }
        public decimal PriceRuble { get; set; }
        public decimal PriceDollar { get; set; }
        public bool Hidden { get; set; }

        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }
    }
}
