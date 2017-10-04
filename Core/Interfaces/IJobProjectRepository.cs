using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IJobProjectRepository
    {
        Task<IEnumerable<JobProjectDomainModel>> GetAll();
        Task<JobProjectDomainModel> GetById(int id);
        Task<IEnumerable<JobProjectDomainModel>> GetByJobId(int jobId);
        Task<JobProjectDomainModel> Save(JobProjectDomainModel jobProject);
        void Delete(int id);
    }
}