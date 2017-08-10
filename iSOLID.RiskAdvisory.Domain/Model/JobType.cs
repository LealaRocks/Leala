using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class JobType : AggregateRoot
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
    }
}
