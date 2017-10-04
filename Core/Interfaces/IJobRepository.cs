using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IJobRepository
    {
        Task<IEnumerable<JobDomainModel>> GetAll();
        Task<JobDomainModel> GetById(int id);
        Task<JobDomainModel> Save(JobDomainModel job);
        void Delete(int id);
    }
}