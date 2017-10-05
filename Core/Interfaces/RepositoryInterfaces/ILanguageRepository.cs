using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces.RepositoryInterfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<LanguageDomainModel>> GetAll();
        Task<LanguageDomainModel> GetById(int id);
        Task<LanguageDomainModel> Save(LanguageDomainModel language);
        void Delete(int id);
    }
}