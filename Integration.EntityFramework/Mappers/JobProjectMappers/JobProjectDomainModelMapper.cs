using System.Collections.Generic;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobProjectMappers
{
  public static class JobProjectDomainModelMapper
  {
    public static Core.Models.JobProject MapFrom(JobProject databaseModel)
    {
      return new Core.Models.JobProject
      {
        Id = databaseModel.Id,
        JobId = databaseModel.JobId,
        Name = databaseModel.Name,
        Description = databaseModel.Description
      };
    }

    public static IEnumerable<Core.Models.JobProject> MapFrom(IEnumerable<JobProject> databaseModels)
    {
      foreach (var databaseModel in databaseModels)
      {
        yield return MapFrom(databaseModel);
      }
    }
  }
}