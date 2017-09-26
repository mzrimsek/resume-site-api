using System.Collections.Generic;
using Core.Models;
using Web.Models;

namespace Web.Mappers.JobMappers
{
    public static class JobViewModelMapper
    {
        public static JobViewModel MapFrom(JobDomainModel domainModel)
        {
            return new JobViewModel()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                City = domainModel.City,
                State = domainModel.State,
                Title = domainModel.Title,
                StartDate = domainModel.StartDate.ToString("M/d/yyyy"),
                EndDate = domainModel.EndDate.ToString("M/d/yyyy")
            };
        }

        public static IEnumerable<JobViewModel> MapFrom(IEnumerable<JobDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }

        public static JobViewModel MapFrom(int id, AddUpdateJobViewModel addUpdateJobViewModel)
        {
            return new JobViewModel()
            {
                Id = id,
                Name = addUpdateJobViewModel.Name,
                City = addUpdateJobViewModel.City,
                State = addUpdateJobViewModel.State,
                Title = addUpdateJobViewModel.Title,
                StartDate = addUpdateJobViewModel.StartDate,
                EndDate = addUpdateJobViewModel.EndDate
            };
        }
    }
}