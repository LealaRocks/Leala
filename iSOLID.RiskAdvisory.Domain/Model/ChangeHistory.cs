using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class ChangeHistory
    {
        public DateTime TimeChanged { get; set; }

        public int UserId { get; set; }
    }
}
