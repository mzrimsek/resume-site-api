using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ISkillRepository : IRepository<SkillDomainModel>
    {
        Task<IEnumerable<LanguageDomainModel>> GetByLanguageId(int jobId);
    }
}