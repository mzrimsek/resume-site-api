using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobProjectMappers
{
    public static class JobProjectDomainModelMapper
    {
        public static JobProjectDomainModel MapFrom(JobProject databaseModel)
        {
            return new JobProjectDomainModel
            {
                Id = databaseModel.Id,
                JobId = databaseModel.JobId,
                Name = databaseModel.Name,
                Description = databaseModel.Description
            };
        }

        public static IEnumerable<JobProjectDomainModel> MapFrom(IEnumerable<JobProject> databaseModels)
        {
            foreach (var databaseModel in databaseModels)
            {
                yield return MapFrom(databaseModel);
            }
        }
    }
}