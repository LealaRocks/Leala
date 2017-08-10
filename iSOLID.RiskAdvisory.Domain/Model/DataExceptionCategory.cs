using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class DataExceptionCategory : Entity
    {
        public DataExceptionCategory()
        {
            this.Exceptions = new List<DataException>();
        }

        public List<DataException> Exceptions { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
    }
}
