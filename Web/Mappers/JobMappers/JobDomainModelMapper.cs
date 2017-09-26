using System;
using System.Collections.Generic;
using Core.Models;
using Web.Models.JobModels;

namespace Web.Mappers.JobMappers
{
    public static class JobDomainModelMapper
    {
        public static JobDomainModel MapFrom(JobViewModel viewModel)
        {
            return new JobDomainModel()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                City = viewModel.City,
                State = viewModel.State,
                Title = viewModel.Title,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate)
            };
        }

        public static IEnumerable<JobDomainModel> MapFrom(IEnumerable<JobViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                yield return MapFrom(viewModel);
            }
        }

        public static JobDomainModel MapFrom(AddUpdateJobViewModel viewModel)
        {
            return new JobDomainModel()
            {
                Name = viewModel.Name,
                City = viewModel.City,
                State = viewModel.State,
                Title = viewModel.Title,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate)
            };
        }

        public static IEnumerable<JobDomainModel> MapFrom(IEnumerable<AddUpdateJobViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                yield return MapFrom(viewModel);
            }
        }
    }
}