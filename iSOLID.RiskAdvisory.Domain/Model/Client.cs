using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public class Client : AggregateRoot
    {
        public string Name { get; set; }
    }
}
