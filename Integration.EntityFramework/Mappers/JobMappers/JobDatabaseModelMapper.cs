using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.JobMappers
{
    public static class JobDatabaseModelMapper
    {
        public static Job MapFrom(JobDomainModel domainModel)
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

        public static IEnumerable<Job> MapFrom(IEnumerable<JobDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }
    }
}