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
    public class SkillRepository : ISkillRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public SkillRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SkillDomainModel>> GetAll()
        {
            return await _databaseContext.Skills.Where(x => true).ProjectTo<SkillDomainModel>().ToListAsync();
        }

        public async Task<SkillDomainModel> GetById(int id)
        {
            var skill = await _databaseContext.Skills.Where(x => x.Id == id).ProjectTo<SkillDomainModel>().FirstOrDefaultAsync();
            return skill == null ? null : skill;
        }

        public async Task<IEnumerable<SkillDomainModel>> GetByLanguageId(int languageId)
        {
            return await _databaseContext.Skills.Where(x => x.LanguageId == languageId).ProjectTo<SkillDomainModel>().ToListAsync();
        }

        public async Task<SkillDomainModel> Save(SkillDomainModel entity)
        {
            var databaseModel = _mapper.Map<SkillDatabaseModel>(entity);
            var existingModel = await _databaseContext.Skills.SingleOrDefaultAsync(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                await _databaseContext.AddAsync(databaseModel);
            }
            else
            {
                existingModel.LanguageId = databaseModel.LanguageId;
                existingModel.Name = databaseModel.Name;
                existingModel.Rating = databaseModel.Rating;

                _databaseContext.Update(existingModel);
            }

            await _databaseContext.SaveChangesAsync();
            return _mapper.Map<SkillDomainModel>(databaseModel);
        }

        public void Delete(int id)
        {
            var skillToDelete = _databaseContext.Skills.SingleOrDefault(x => x.Id == id);
            if (skillToDelete != null)
            {
                _databaseContext.Remove(skillToDelete);
                _databaseContext.SaveChanges();
            }
        }

    }
}