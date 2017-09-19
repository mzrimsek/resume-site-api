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

    public IEnumerable<Core.Models.School> GetAll()
    {
      var schools = _databaseContext.Schools.ToList();
      return SchoolDomainModelMapper.MapFrom(schools);
    }

    public Core.Models.School GetById(int id)
    {
      var school = _databaseContext.Schools.SingleOrDefault(x => x.Id == id);
      return SchoolDomainModelMapper.MapFrom(school);
    }
  }
}