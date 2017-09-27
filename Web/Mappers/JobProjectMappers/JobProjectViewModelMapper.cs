using System.Collections.Generic;
using Core.Models;
using Web.Models.JobProjectModels;

namespace Web.Mappers.JobProjectMappers
{
    public static class JobProjectViewModelMapper
    {
        public static JobProjectViewModel MapFrom(JobProjectDomainModel domainModel)
        {
            return new JobProjectViewModel()
            {
                Id = domainModel.Id,
                JobId = domainModel.JobId,
                Name = domainModel.Name,
                Description = domainModel.Description
            };
        }

        public static IEnumerable<JobProjectViewModel> MapFrom(IEnumerable<JobProjectDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }

        public static JobProjectViewModel MapFrom(int id, AddUpdateJobProjectViewModel viewModel)
        {
            return new JobProjectViewModel()
            {
                Id = id,
                JobId = viewModel.JobId,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }
    }
}