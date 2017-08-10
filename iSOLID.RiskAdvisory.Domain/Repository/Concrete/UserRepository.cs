using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MongoDB.Driver;

namespace iSOLID.RiskAdvisory.Domain.Repository.Concrete
{
    public class UserRepository : MongoRepositoryBase<User>, IUserRepository
    {

        public UserRepository(IMongoClient mongoClient) 
            : base(mongoClient) { }

        public User this[string email]
        {
            get { return base.QueryCollection(x => x.Email == email).SingleOrDefault(); }
        }

        public IEnumerable<User> All()
        {
            return this.QueryCollection();
        }

        public User GetByApiKey(string apiKey)
        {
            Guid key;

            if(!String.IsNullOrEmpty(apiKey) && Guid.TryParse(apiKey, out key))
            {
                return base.QueryCollection(x => x.ApiKey == key).SingleOrDefault();
            }

            return null;
        }

        public IEnumerable<User> GetForProject(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
