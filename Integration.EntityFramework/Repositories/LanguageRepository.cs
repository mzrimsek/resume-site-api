using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Helpers;
using Integration.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RepositoryHelper<LanguageDomainModel, LanguageDatabaseModel> _repositoryHelper;
        public LanguageRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _repositoryHelper = new RepositoryHelper<LanguageDomainModel, LanguageDatabaseModel>(databaseContext.Languages, mapper);
        }

        public async Task<IEnumerable<LanguageDomainModel>> GetAll()
        {
            return await _repositoryHelper.GetAll();
        }

        public async Task<LanguageDomainModel> GetById(int id)
        {
            return await _repositoryHelper.GetById(id);
        }

        public async Task<LanguageDomainModel> Save(LanguageDomainModel entity)
        {
            var language = await _repositoryHelper.Save(entity);
            await _databaseContext.SaveChangesAsync();
            return language;
        }

        public async void Delete(int id)
        {
            var languageToDelete = await _databaseContext.Languages.SingleOrDefaultAsync(x => x.Id == id);
            if (languageToDelete != null)
            {
                var skillsForLanguage = await _databaseContext.Skills.Where(x => x.LanguageId == id).ToListAsync();
                _databaseContext.RemoveRange(skillsForLanguage);
                _databaseContext.Remove(languageToDelete);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}