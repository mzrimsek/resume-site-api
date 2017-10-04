using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Integration.EntityFramework.Mappers.SchoolMappers;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly DatabaseContext _databaseContext;

        public SchoolRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<SchoolDomainModel>> GetAll()
        {
            var schools = await _databaseContext.Schools.ToListAsync();
            return SchoolDomainModelMapper.MapFrom(schools);
        }

        public async Task<SchoolDomainModel> GetById(int id)
        {
            var school = await _databaseContext.Schools.SingleOrDefaultAsync(x => x.Id == id);
            return school == null ? null : SchoolDomainModelMapper.MapFrom(school);
        }

        public async Task<SchoolDomainModel> Save(SchoolDomainModel school)
        {
            var databaseModel = SchoolDatabaseModelMapper.MapFrom(school);
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
            return SchoolDomainModelMapper.MapFrom(databaseModel);
        }

        public async void Delete(int id)
        {
            var schoolToDelete = await _databaseContext.Schools.SingleOrDefaultAsync(x => x.Id == id);
            if (schoolToDelete != null)
            {
                _databaseContext.Remove(schoolToDelete);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}