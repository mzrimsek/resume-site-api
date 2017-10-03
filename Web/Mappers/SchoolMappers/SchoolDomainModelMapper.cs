using System;
using System.Collections.Generic;
using Core.Models;
using Web.Models.SchoolModels;

namespace Web.Mappers.SchoolMappers
{
    public static class SchoolDomainModelMapper
    {
        public static SchoolDomainModel MapFrom(SchoolViewModel viewModel)
        {
            return new SchoolDomainModel()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                City = viewModel.City,
                State = viewModel.State,
                Major = viewModel.Major,
                Degree = viewModel.Degree,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate)
            };
        }

        public static IEnumerable<SchoolDomainModel> MapFrom(IEnumerable<SchoolViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                yield return MapFrom(viewModel);
            }
        }

        public static SchoolDomainModel MapFrom(AddSchoolViewModel viewModel)
        {
            return new SchoolDomainModel()
            {
                Name = viewModel.Name,
                City = viewModel.City,
                State = viewModel.State,
                Major = viewModel.Major,
                Degree = viewModel.Degree,
                StartDate = DateTime.Parse(viewModel.StartDate),
                EndDate = DateTime.Parse(viewModel.EndDate)
            };
        }
    }
}