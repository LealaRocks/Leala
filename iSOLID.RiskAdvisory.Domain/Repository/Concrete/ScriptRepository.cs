using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using System.Linq;

namespace iSOLID.RiskAdvisory.Domain.Repository.Concrete
{
    public class ScriptRepository : MongoRepositoryBase<Script>, IScriptRepository
    {
        public ScriptRepository(IMongoClient client) 
            : base(client)
        {

        }

        public Script this[string name]
        {
            get { return this.QueryCollection(x => x.Name == name).SingleOrDefault(); }
        }

        public IEnumerable<Script> All()
        {
            return QueryCollection();
        }
    }
}
