using System.Collections.Generic;
using Core.Models;
using Web.Models;

namespace Web.Mappers.JobMappers
{
    public static class JobViewModelMapper
    {
        public static Job MapFrom(JobDomainModel domainModel)
        {
            return new Job()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                City = domainModel.City,
                State = domainModel.State,
                Title = domainModel.Title,
                StartDate = domainModel.StartDate.ToShortDateString(),
                EndDate = domainModel.EndDate.ToShortDateString()
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