using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ILanguageRepository
    {
        IEnumerable<LanguageDomainModel> GetAll();
        LanguageDomainModel GetById(int id);
        LanguageDomainModel Save(LanguageDomainModel job);
        void Delete(int id);
    }
}