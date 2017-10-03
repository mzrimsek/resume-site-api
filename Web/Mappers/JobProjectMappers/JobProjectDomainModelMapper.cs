using System;
using System.Collections.Generic;
using Core.Models;
using Web.Models.JobProjectModels;

namespace Web.Mappers.JobProjectMappers
{
    public static class JobProjectDomainModelMapper
    {
        public static JobProjectDomainModel MapFrom(JobProjectViewModel viewModel)
        {
            return new JobProjectDomainModel()
            {
                Id = viewModel.Id,
                JobId = viewModel.JobId,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }

        public static IEnumerable<JobProjectDomainModel> MapFrom(IEnumerable<JobProjectViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                yield return MapFrom(viewModel);
            }
        }

        public static JobProjectDomainModel MapFrom(AddJobProjectViewModel viewModel)
        {
            return new JobProjectDomainModel()
            {
                JobId = viewModel.JobId,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
        }
    }
}