using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace iSOLID.RiskAdvisory.Domain.Repository.Concrete
{
    public abstract class MongoRepositoryBase<TAggregate> : IRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        protected class Dto
        {
            public Dto()
            {
                this.History = new Stack<TAggregate>();
            }

            public Stack<TAggregate> History { get; set; }

            public Guid Id { get; set; }

            public TAggregate CurrentVersion { get; set; }

            public bool Deleted { get; set; }

            public void SetNewVersion(TAggregate aggregate)
            {
                if(this.CurrentVersion != null)
                {
                    this.History.Push(this.CurrentVersion);
                    aggregate.Version++;
                }
                else
                {
                    this.Id = aggregate.Id;
                    aggregate.Version = 1;
                }

                this.CurrentVersion = aggregate;
            }
            
        }

        private readonly IMongoClient client;
        private const string DEFAULT_DATABASE = "Leala";

        public MongoRepositoryBase(IMongoClient client)
        {
            this.client = client;
        }

        protected IMongoClient DbClient
        {
            get { return this.client;  }
        }

        protected virtual string DbName
        {
            get { return DEFAULT_DATABASE; }
        }


        protected virtual string CollectionName
        {
            get { return typeof(TAggregate).Name; }
        }

        protected IQueryable<TAggregate> QueryCollection()
        {
            return this.QueryCollection(null);
        }

        protected IQueryable<TAggregate> QueryCollection(Expression<Func<TAggregate, bool>> predicate)
        {

            var collection = GetDtoCollection().AsQueryable();

            var query = from dto in collection
                        where !dto.Deleted
                        select dto.CurrentVersion;

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }

        protected IMongoCollection<Dto> GetDtoCollection()
        {
            var db = DbClient.GetDatabase(DbName);
            var collection = db.GetCollection<Dto>(CollectionName, null);

            return collection;
        }

        public virtual TAggregate this[Guid id]
        {
            get
            {
                var dto =  GetDtoCollection().AsQueryable().ToArray().FirstOrDefault(x => x.Id == id);

                if (dto == null || dto.Deleted)
                    return null;

                else
                {
                    return dto.CurrentVersion;
                }

            }
        }

        public void Add(TAggregate aggregate)
        {
            if (aggregate.Id == Guid.Empty)
                throw new ArgumentException("Aggregate Must Have Id: ", nameof(aggregate));

            var collection = GetDtoCollection();

            var dto = new Dto();
            dto.SetNewVersion(aggregate);
            collection.InsertOne(dto);
        }

        public void Delete(TAggregate aggregate)
        {
            var collection = this.GetDtoCollection();

            var dto = collection.AsQueryable().Where(x => x.Id == aggregate.Id).Single();

            if (!dto.Deleted)
            {
                dto.Deleted = true;
                collection.FindOneAndReplace(x=> x.Id == aggregate.Id, dto);
            }
        }

        public void Update(TAggregate aggregate)
        {
            var db = DbClient.GetDatabase(DbName);
            var collection = db.GetCollection<Dto>(CollectionName, null);

            var dto = collection.Find(x => x.Id == aggregate.Id && !x.Deleted).Single();
            
            if(dto.CurrentVersion.Version != aggregate.Version)
            {
                throw new ConcurrencyException(aggregate.Id, aggregate.Version, dto.CurrentVersion.Version);
            }

            dto.SetNewVersion(aggregate);
            collection.FindOneAndReplace(x => x.Id == aggregate.Id && !x.Deleted, dto);
        }
    }
}
