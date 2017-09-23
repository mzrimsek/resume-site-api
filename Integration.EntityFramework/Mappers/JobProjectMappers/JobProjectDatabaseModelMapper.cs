using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobProjectMappers
{
    public class JobProjectDatabaseModelMapper
    {
        public static JobProjectDatabaseModel MapFrom(JobProjectDomainModel domainModel)
        {
            return new JobProjectDatabaseModel
            {
                Id = domainModel.Id,
                JobId = domainModel.JobId,
                Name = domainModel.Name,
                Description = domainModel.Description
            };
        }

        public static IEnumerable<JobProjectDatabaseModel> MapFrom(IEnumerable<JobProjectDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }
    }
}