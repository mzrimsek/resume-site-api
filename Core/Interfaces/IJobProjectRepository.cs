using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface IJobProjectRepository
    {
        IEnumerable<JobProjectDomainModel> GetAll();
        JobProjectDomainModel GetById(int id);
        IEnumerable<JobProjectDomainModel> GetByJobId(int jobId);
        JobProjectDomainModel Save(JobProjectDomainModel jobProject);
        void Delete(int id);
    }
}