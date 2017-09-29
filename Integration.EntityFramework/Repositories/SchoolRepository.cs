using Microsoft.EntityFrameworkCore;
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

            _databaseContext.Add(databaseModel);
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

        public SchoolDomainModel Update(SchoolDomainModel school)
        {
            var databaseModel = SchoolDatabaseModelMapper.MapFrom(school);
            var existingModel = _databaseContext.Schools.SingleOrDefault(x => x.Id == databaseModel.Id);
            existingModel = databaseModel;

            _databaseContext.Entry(existingModel).State = EntityState.Modified;
            _databaseContext.SaveChanges();

            return SchoolDomainModelMapper.MapFrom(existingModel);
        }
    }
}