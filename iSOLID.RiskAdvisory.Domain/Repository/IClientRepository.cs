using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Repository
{
    public interface IClientRepository : IRepository<Client>
    {
        IEnumerable<Client> All();
    }
}
