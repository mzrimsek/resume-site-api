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
                StartDate = DateHelper.FormatDate(domainModel.StartDate),
                EndDate = DateHelper.FormatDate(domainModel.EndDate)
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