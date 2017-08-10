using iSOLID.RiskAdvisory.Domain.Model;
using iSOLID.RiskAdvisory.Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public class JobTypeService : IJobTypeService
    {
        private readonly IJobTypeRepository jobTypes;

        public JobTypeService(IJobTypeRepository jobTypes)
        {
            this.jobTypes = jobTypes;
        }

        public IEnumerable<JobType> GetAllJobTypes()
        {
            return jobTypes.All();
        }

        public bool TryCreateJobType(JobType jobType, out List<ValidationResult> validationResults)
        {
            if (jobType == null)
                throw new ArgumentNullException(nameof(jobType));

            validationResults = new List<ValidationResult>();

            var existing = jobTypes[jobType.Name];

            var valid = Validator.TryValidateObject(jobType, new ValidationContext(jobType), validationResults, true);

            if (existing != null)
            {
                validationResults.Add(new ValidationResult("This job type with this name already exists", new string[] { "Name" }));
                valid = false;
            }

            if (valid)
            {
                jobType.Id = Guid.NewGuid();
                jobTypes.Add(jobType);
            }

            return valid;
        }

        public bool TryUpdateJobType(JobType jobType, out List<ValidationResult> validationResults)
        {
            if (jobType == null)
                throw new ArgumentNullException(nameof(jobType));

            if (jobType.Id == Guid.Empty)
                throw new ArgumentException("Update requires Job Type Id", nameof(jobType));

            validationResults = new List<ValidationResult>();

            var existing = jobTypes[jobType.Name];

            var valid = Validator.TryValidateObject(jobType, new ValidationContext(jobType), validationResults, true);

            if(existing != null && existing.Id != jobType.Id)
            {
                validationResults.Add(new ValidationResult("This job type with this name already exists", new string[] { "Name" }));
                valid = false;
            }

            if (valid)
            { 
                jobTypes.Update(jobType);
            }

            return valid;
        }
    }
}
