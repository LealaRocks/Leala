using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Repository
{
    public interface IRepository<T>
        where T: AggregateRoot
    {
        T this[Guid id] { get; }

        void Update(T aggregate);

        void Add(T aggregate);

        void Delete(T aggregate);

    }
}
