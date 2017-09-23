using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobMappers
{
    public static class JobDatabaseModelMapper
    {
        public static JobDatabaseModel MapFrom(JobDomainModel domainModel)
        {
            return new JobDatabaseModel
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

        public static IEnumerable<JobDatabaseModel> MapFrom(IEnumerable<JobDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }
    }
}