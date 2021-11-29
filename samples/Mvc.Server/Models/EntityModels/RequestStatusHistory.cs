using System;
using System.ComponentModel.DataAnnotations;

namespace Mvc.Server.Models
{
    public class RequestStatusHistory
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateTime { get; set; }

        public Guid StatusId { get; set; }
        public Guid RequestId { get; set; }

        public virtual Status Status { get; set; }
        public virtual Request Request { get; set; }
    }
}