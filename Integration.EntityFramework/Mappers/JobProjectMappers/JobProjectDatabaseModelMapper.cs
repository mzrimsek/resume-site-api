using System.Collections.Generic;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobProjectMappers
{
    public class JobProjectDatabaseModelMapper
    {
        public static JobProject MapFrom(Core.Models.JobProject domainModel)
        {
            return new JobProject
            {
                Id = domainModel.Id,
                JobId = domainModel.JobId,
                Name = domainModel.Name,
                Description = domainModel.Description
            };
        }

        public static IEnumerable<JobProject> MapFrom(IEnumerable<Core.Models.JobProject> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }
    }
}