using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.Mappers.JobMappers
{
    public static class JobDomainModelMapper
    {
        public static Core.Models.Job MapFrom(Job viewModel)
        {
            return new Core.Models.Job()
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

        public static IEnumerable<Core.Models.Job> MapFrom(IEnumerable<Job> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                yield return MapFrom(viewModel);
            }
        }
    }
}