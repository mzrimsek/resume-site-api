using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface IJobProjectRepository : IRepository<JobProjectDomainModel>
    {
        Task<IEnumerable<JobProjectDomainModel>> GetByJobId(int jobId);
    }
}