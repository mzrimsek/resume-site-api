using System.Collections.Generic;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.SchoolMappers
{
  public static class SchoolDomainModelMapper
  {
    public static Core.Models.School MapFrom(School databaseModel)
    {
      return new Core.Models.School
      {
        Id = databaseModel.Id,
        Name = databaseModel.Name,
        City = databaseModel.City,
        State = databaseModel.State,
        Major = databaseModel.Major,
        Degree = databaseModel.Degree,
        StartDate = databaseModel.StartDate,
        EndDate = databaseModel.EndDate
      };
    }

    public static IEnumerable<Core.Models.School> MapFrom(IEnumerable<School> databaseModels)
    {
      foreach (var databaseModel in databaseModels)
      {
        yield return MapFrom(databaseModel);
      }
    }
  }
}