using System.Collections.Generic;
using Core.Helpers;
using Core.Models;
using Web.Models.JobModels;

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
                StartDate = domainModel.StartDate.Format(),
                EndDate = domainModel.EndDate.Format()
            };
        }

        public static IEnumerable<JobViewModel> MapFrom(IEnumerable<JobDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }

        public static JobViewModel MapFrom(int id, AddJobViewModel viewModel)
        {
            return new JobViewModel()
            {
                Id = id,
                Name = viewModel.Name,
                City = viewModel.City,
                State = viewModel.State,
                Title = viewModel.Title,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };
        }
    }
}