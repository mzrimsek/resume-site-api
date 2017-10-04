using System.Collections.Generic;
using Core.Helpers;
using Core.Models;
using Web.Models.SchoolModels;

namespace Web.Mappers.SchoolMappers
{
    public static class SchoolViewModelMapper
    {
        public static SchoolViewModel MapFrom(SchoolDomainModel domainModel)
        {
            return new SchoolViewModel()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                City = domainModel.City,
                State = domainModel.State,
                Major = domainModel.Major,
                Degree = domainModel.Degree,
                StartDate = domainModel.StartDate.Format(),
                EndDate = domainModel.EndDate.Format()
            };
        }

        public static IEnumerable<SchoolViewModel> MapFrom(IEnumerable<SchoolDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }

        public static SchoolViewModel MapFrom(int id, AddSchoolViewModel viewModel)
        {
            return new SchoolViewModel()
            {
                Id = id,
                Name = viewModel.Name,
                City = viewModel.City,
                State = viewModel.State,
                Major = viewModel.Major,
                Degree = viewModel.Degree,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate
            };
        }
    }
}