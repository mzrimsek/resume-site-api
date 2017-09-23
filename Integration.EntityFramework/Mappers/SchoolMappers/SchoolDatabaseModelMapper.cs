using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.SchoolMappers
{
    public static class SchoolDatabaseModelMapper
    {
        public static SchoolDatabaseModel MapFrom(SchoolDomainModel domainModel)
        {
            return new SchoolDatabaseModel
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                City = domainModel.City,
                State = domainModel.State,
                Major = domainModel.Major,
                Degree = domainModel.Degree,
                StartDate = domainModel.StartDate,
                EndDate = domainModel.EndDate
            };
        }

        public static IEnumerable<SchoolDatabaseModel> MapFrom(IEnumerable<SchoolDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }
    }
}