using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iSOLID.RiskAdvisory.Domain.Model;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public interface IJobTypeService
    {
        IEnumerable<JobType> GetAllJobTypes();
        bool TryCreateJobType(JobType jobType, out List<ValidationResult> validationResults);
        bool TryUpdateJobType(JobType jobType, out List<ValidationResult> validationResults);
    }
}