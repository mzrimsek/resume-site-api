using System.Collections.Generic;
using Web.Models;

namespace Web.Mappers.JobMappers
{
  public static class JobViewModelMapper
  {
    public static Job MapFrom(Core.Models.Job domainModel)
    {
      return new Job()
      {
        Name = domainModel.Name,
        City = domainModel.City,
        State = domainModel.State,
        Title = domainModel.Title,
        StartDate = domainModel.StartDate.ToShortDateString(),
        EndDate = domainModel.EndDate.ToShortDateString()
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