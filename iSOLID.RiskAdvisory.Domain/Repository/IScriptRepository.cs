using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Repository
{
    public interface IScriptRepository : IRepository<Script>
    {
        IEnumerable<Script> All();

        Script this[string name] { get; }
    }
}
