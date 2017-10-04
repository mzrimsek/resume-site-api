using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<LanguageDomainModel>> GetAll()
        {
            var languages = await _databaseContext.Languages.ToListAsync();
            return LanguageDomainModelMapper.MapFrom(languages);
        }

        public async Task<LanguageDomainModel> GetById(int id)
        {
            var language = await _databaseContext.Languages.SingleOrDefaultAsync(x => x.Id == id);
            return language == null ? null : LanguageDomainModelMapper.MapFrom(language);
        }

        public async Task<LanguageDomainModel> Save(LanguageDomainModel language)
        {
            var databaseModel = LanguageDatabaseModelMapper.MapFrom(language);
            var existingModel = await _databaseContext.Languages.SingleOrDefaultAsync(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                await _databaseContext.AddAsync(databaseModel);
            }
            else
            {
                existingModel.Name = databaseModel.Name;
                existingModel.Rating = databaseModel.Rating;

                _databaseContext.Update(existingModel);
            }

            await _databaseContext.SaveChangesAsync();
            return LanguageDomainModelMapper.MapFrom(databaseModel);
        }

        public async void Delete(int id)
        {
            var languageToDelete = await _databaseContext.Languages.SingleOrDefaultAsync(x => x.Id == id);
            if (languageToDelete != null)
            {
                _databaseContext.Remove(languageToDelete);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}