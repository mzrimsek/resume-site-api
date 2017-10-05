using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ISchoolRepository
    {
        Task<IEnumerable<SchoolDomainModel>> GetAll();
        Task<SchoolDomainModel> GetById(int id);
        Task<SchoolDomainModel> Save(SchoolDomainModel school);
        void Delete(int id);
    }
}