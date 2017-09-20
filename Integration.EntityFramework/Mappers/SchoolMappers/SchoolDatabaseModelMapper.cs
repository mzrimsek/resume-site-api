using System.Collections.Generic;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.SchoolMappers
{
    public static class SchoolDatabaseModelMapper
    {
        public static School MapFrom(Core.Models.School domainModel)
        {
            return new School
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

        public static IEnumerable<School> MapFrom(IEnumerable<Core.Models.School> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }
    }
}