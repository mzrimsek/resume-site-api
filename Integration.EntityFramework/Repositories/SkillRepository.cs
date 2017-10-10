using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Models;
using Integration.EntityFramework.Helpers;
using Integration.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Integration.EntityFramework.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly RepositoryHelper<SkillDomainModel, SkillDatabaseModel> _repositoryHelper;
        public SkillRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _repositoryHelper = new RepositoryHelper<SkillDomainModel, SkillDatabaseModel>(databaseContext.Skills, mapper);
        }

        public async Task<IEnumerable<SkillDomainModel>> GetAll()
        {
            return await _repositoryHelper.GetAll();
        }

        public async Task<SkillDomainModel> GetById(int id)
        {
            return await _repositoryHelper.GetById(id);
        }

        public async Task<IEnumerable<SkillDomainModel>> GetByLanguageId(int languageId)
        {
            return await _databaseContext.Skills.Where(x => x.LanguageId == languageId).ProjectTo<SkillDomainModel>().ToListAsync();
        }

        public async Task<SkillDomainModel> Save(SkillDomainModel entity)
        {
            var skill = await _repositoryHelper.Save(entity);
            await _databaseContext.SaveChangesAsync();
            return skill;
        }

        public async void Delete(int id)
        {
            _repositoryHelper.Delete(id);
            await _databaseContext.SaveChangesAsync();
        }

    }
}