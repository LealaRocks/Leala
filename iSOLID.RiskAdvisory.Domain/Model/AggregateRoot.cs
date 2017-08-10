using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public abstract class AggregateRoot : Entity
    {
        public AggregateRoot()
        {
            this.Version = 1;
        }

        public long Version { get; set; }
    }
}
