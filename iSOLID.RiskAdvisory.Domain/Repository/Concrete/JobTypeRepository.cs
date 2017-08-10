using iSOLID.RiskAdvisory.Domain.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace iSOLID.RiskAdvisory.Domain.Repository.Concrete
{
    public class JobTypeRepository : MongoRepositoryBase<JobType>, IJobTypeRepository
    {
        public JobTypeRepository(IMongoClient client)
            :base(client)
        {

        }

        public JobType this[string name]
        {
            get { return QueryCollection(x => x.Name == name).SingleOrDefault(); }
        }

        public IEnumerable<JobType> All()
        {
            return this.QueryCollection();
        }
    }
}
