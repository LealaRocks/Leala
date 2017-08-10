using iSOLID.RiskAdvisory.Domain.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace iSOLID.RiskAdvisory.Domain.Repository.Concrete
{
    public class ClientRepository : MongoRepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(IMongoClient client)
            : base(client)
        {
        }

        public IEnumerable<Client> All()
        {
            return base.QueryCollection();
        }
    }
}
