using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Model
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
