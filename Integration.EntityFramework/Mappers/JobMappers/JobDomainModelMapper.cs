using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobMappers
{
    public static class JobDomainModelMapper
    {
        public static JobDomainModel MapFrom(JobDatabaseModel databaseModel)
        {
            return new JobDomainModel
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

        public static IEnumerable<JobDomainModel> MapFrom(IEnumerable<JobDatabaseModel> databaseModels)
        {
            foreach (var databaseModel in databaseModels)
            {
                yield return MapFrom(databaseModel);
            }
        }
    }
}