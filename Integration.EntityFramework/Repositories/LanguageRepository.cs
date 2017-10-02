using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Models;
using Integration.EntityFramework.Mappers.LanguageMappers;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DatabaseContext _databaseContext;
        public LanguageRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<LanguageDomainModel> GetAll()
        {
            var languages = _databaseContext.Languages.ToList();
            return LanguageDomainModelMapper.MapFrom(languages);
        }

        public LanguageDomainModel GetById(int id)
        {
            var language = _databaseContext.Languages.SingleOrDefault(x => x.Id == id);
            return language == null ? null : LanguageDomainModelMapper.MapFrom(language);
        }

        public LanguageDomainModel Save(LanguageDomainModel language)
        {
            var databaseModel = LanguageDatabaseModelMapper.MapFrom(language);
            var existingModel = _databaseContext.Languages.SingleOrDefault(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                _databaseContext.Add(databaseModel);
            }
            else
            {
                existingModel.Name = databaseModel.Name;
                existingModel.Rating = databaseModel.Rating;

                _databaseContext.Update(existingModel);
            }

            _databaseContext.SaveChanges();

            return LanguageDomainModelMapper.MapFrom(databaseModel);
        }

        public void Delete(int id)
        {
            var languageToDelete = _databaseContext.Languages.SingleOrDefault(x => x.Id == id);
            if (languageToDelete != null)
            {
                _databaseContext.Remove(languageToDelete);
                _databaseContext.SaveChanges();
            }
        }
    }
}