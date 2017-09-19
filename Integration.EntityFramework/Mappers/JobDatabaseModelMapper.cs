using System.Collections.Generic;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers
{
  public static class JobDatabaseModelMapper
  {
    public static Job MapFrom(Core.Models.Job domainModel)
    {
      return new Job
      {
        Id = domainModel.Id,
        Name = domainModel.Name,
        City = domainModel.City,
        State = domainModel.State,
        Title = domainModel.Title,
        StartDate = domainModel.StartDate,
        EndDate = domainModel.EndDate
      };
    }

    public static IEnumerable<Job> MapFrom(IEnumerable<Core.Models.Job> domainModels)
    {
      foreach (var domainModel in domainModels)
      {
        yield return MapFrom(domainModel);
      }
    }
  }
}