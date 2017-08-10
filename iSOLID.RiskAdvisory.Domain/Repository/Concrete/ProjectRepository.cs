using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MongoDB.Driver;

namespace iSOLID.RiskAdvisory.Domain.Repository.Concrete
{
    public class ProjectRepository : MongoRepositoryBase<Project>, IProjectRepository
    {
        public ProjectRepository(IMongoClient mongoClient)
            : base (mongoClient)
        { }

        public Project this[string name]
        {
            get { return base.QueryCollection(x => x.Name == name).SingleOrDefault();  }
        }

        public IEnumerable<Project> GetProjectsForUser(string userName)
        {
            var db = this.DbClient.GetDatabase(this.DbName);
            var collection = db.GetCollection<Dto>(this.CollectionName);

            return base.QueryCollection(x => x.Users.Any(y => y.UserName == userName));
        }
    }
}
