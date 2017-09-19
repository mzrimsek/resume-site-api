using System.Collections.Generic;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobMappers
{
  public static class JobDomainModelMapper
  {
    public static Core.Models.Job MapFrom(Job databaseModel)
    {
      return new Core.Models.Job
      {
        Id = databaseModel.Id,
        Name = databaseModel.Name,
        City = databaseModel.City,
        State = databaseModel.State,
        Title = databaseModel.Title,
        StartDate = databaseModel.StartDate,
        EndDate = databaseModel.EndDate
      };
    }

    public static IEnumerable<Core.Models.Job> MapFrom(IEnumerable<Job> databaseModels)
    {
      foreach (var databaseModel in databaseModels)
      {
        yield return MapFrom(databaseModel);
      }
    }
  }
}