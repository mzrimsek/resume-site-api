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
                StartDate = DateHelper.FormatDate(domainModel.StartDate),
                EndDate = DateHelper.FormatDate(domainModel.EndDate)
            };
        }

        public static IEnumerable<SchoolViewModel> MapFrom(IEnumerable<SchoolDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }

        public static SchoolViewModel MapFrom(int id, AddUpdateSchoolViewModel addUpdateSchoolViewModel)
        {
            return new SchoolViewModel()
            {
                Id = id,
                Name = addUpdateSchoolViewModel.Name,
                City = addUpdateSchoolViewModel.City,
                State = addUpdateSchoolViewModel.State,
                Major = addUpdateSchoolViewModel.Major,
                Degree = addUpdateSchoolViewModel.Degree,
                StartDate = addUpdateSchoolViewModel.StartDate,
                EndDate = addUpdateSchoolViewModel.EndDate
            };
        }
    }
}