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
    public class SchoolRepository : ISchoolRepository
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        public SchoolRepository(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SchoolDomainModel>> GetAll()
        {
            return await _databaseContext.Schools.Where(x => true).ProjectTo<SchoolDomainModel>().ToListAsync();
        }

        public async Task<SchoolDomainModel> GetById(int id)
        {
            var school = await _databaseContext.Schools.Where(x => true).ProjectTo<SchoolDomainModel>().SingleOrDefaultAsync(x => x.Id == id);
            return school == null ? null : school;
        }

        public async Task<SchoolDomainModel> Save(SchoolDomainModel entity)
        {
            var databaseModel = _mapper.Map<SchoolDatabaseModel>(entity);
            var existingModel = await _databaseContext.Schools.SingleOrDefaultAsync(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                await _databaseContext.AddAsync(databaseModel);
            }
            else
            {
                existingModel.Name = databaseModel.Name;
                existingModel.City = databaseModel.City;
                existingModel.State = databaseModel.State;
                existingModel.Major = databaseModel.Major;
                existingModel.Degree = databaseModel.Degree;
                existingModel.StartDate = databaseModel.StartDate;
                existingModel.EndDate = databaseModel.EndDate;

                _databaseContext.Update(existingModel);
            }

            await _databaseContext.SaveChangesAsync();
            return _mapper.Map<SchoolDomainModel>(databaseModel);
        }

        public void Delete(int id)
        {
            var schoolToDelete = _databaseContext.Schools.SingleOrDefault(x => x.Id == id);
            if (schoolToDelete != null)
            {
                _databaseContext.Remove(schoolToDelete);
                _databaseContext.SaveChanges();
            }
        }
    }
}