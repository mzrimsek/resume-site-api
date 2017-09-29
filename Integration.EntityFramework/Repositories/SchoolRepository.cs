using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<SchoolDomainModel> GetAll()
        {
            var schools = _databaseContext.Schools.ToList();
            return SchoolDomainModelMapper.MapFrom(schools);
        }

        public SchoolDomainModel GetById(int id)
        {
            var school = _databaseContext.Schools.SingleOrDefault(x => x.Id == id);
            return school == null ? null : SchoolDomainModelMapper.MapFrom(school);
        }

        public SchoolDomainModel Save(SchoolDomainModel school)
        {
            var databaseModel = SchoolDatabaseModelMapper.MapFrom(school);
            var existingModel = _databaseContext.Schools.SingleOrDefault(x => x.Id == databaseModel.Id);
            if (existingModel == null)
            {
                _databaseContext.Add(databaseModel);
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

            _databaseContext.SaveChanges();

            return SchoolDomainModelMapper.MapFrom(databaseModel);
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