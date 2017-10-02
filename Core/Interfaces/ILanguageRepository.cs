using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces
{
    public interface ILanguageRepository
    {
        IEnumerable<LanguageDomainModel> GetAll();
        LanguageDomainModel GetById(int id);
        LanguageDomainModel Save(LanguageDomainModel language);
        void Delete(int id);
    }
}