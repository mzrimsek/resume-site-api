using System;
using System.Collections.Generic;
using Core.Models;
using Web.Models;

namespace Web.Mappers.JobMappers
{
    public static class JobDomainModelMapper
    {
        public static JobDomainModel MapFrom(Job viewModel)
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

        public static IEnumerable<JobDomainModel> MapFrom(IEnumerable<Job> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                yield return MapFrom(viewModel);
            }
        }
    }
}