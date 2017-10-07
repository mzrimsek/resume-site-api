using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public LanguageRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LanguageDomainModel>> GetAll()
        {
            return await _databaseContext.Languages.Where(x => true).ProjectTo<LanguageDomainModel>().ToListAsync();
        }

        public async Task<LanguageDomainModel> GetById(int id)
        {
            return await _databaseContext.Languages.Where(x => x.Id == id).ProjectTo<LanguageDomainModel>().FirstOrDefaultAsync();
        }

        public async Task<LanguageDomainModel> Save(LanguageDomainModel entity)
        {
            var databaseModel = _mapper.Map<LanguageDatabaseModel>(entity);
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
            return _mapper.Map<LanguageDomainModel>(databaseModel);
        }

        public void Delete(int id)
        {
            var languageToDelete = _databaseContext.Languages.SingleOrDefault(x => x.Id == id);
            if (languageToDelete != null)
            {
                var skillsForLanguage = _databaseContext.Skills.Where(x => x.LanguageId == id);
                _databaseContext.RemoveRange(skillsForLanguage);
                _databaseContext.Remove(languageToDelete);
                _databaseContext.SaveChanges();
            }
        }
    }
}