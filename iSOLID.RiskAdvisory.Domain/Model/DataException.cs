using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class DataException : Entity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Code { get; set; }

        public string DisplayText
        {
            get
            {
                if(string.IsNullOrEmpty(this.Name) || string.IsNullOrEmpty(this.Code))
                {
                    return null;
                }

                return string.Format("{0}: ", this.Code, this.Name);
            }
        }
    }
}
