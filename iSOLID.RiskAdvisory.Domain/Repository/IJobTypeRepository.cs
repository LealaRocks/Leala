using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Repository
{
    public interface IJobTypeRepository : IRepository<JobType>
    {
        JobType this[string name] { get; }

        IEnumerable<JobType> All();
        
    }
}
