using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain
{
    public class ConcurrencyException : Exception
    {
        private static string MESSAGE_FORMAT = @"Concurrency Exception For Aggregate With Id: {0}. 
            Expected Version: {1}, 
            Actual Version: {2}";

        public ConcurrencyException(Guid aggregateId, long expectedVersion, long actualVersion)
            : base(String.Format(MESSAGE_FORMAT, aggregateId, expectedVersion, actualVersion))
        {
            this.AggregateId = aggregateId;
            this.ExpectedVersion = expectedVersion;
            this.ActualVersion = actualVersion;
        }

        public Guid AggregateId { get; private set; }

        public long ExpectedVersion { get; private set; }

        public long ActualVersion { get; private set; }
    }
}
