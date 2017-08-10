using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class Tab : Entity
    {
        public Tab()
        {
            this.Controls = new List<Control>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public List<Control> Controls { get; set; }
    }
}
