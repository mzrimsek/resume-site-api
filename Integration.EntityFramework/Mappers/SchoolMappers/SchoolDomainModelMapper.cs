using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.SchoolMappers
{
    public static class SchoolDomainModelMapper
    {
        public static SchoolDomainModel MapFrom(School databaseModel)
        {
            return new SchoolDomainModel
            {
                Id = databaseModel.Id,
                Name = databaseModel.Name,
                City = databaseModel.City,
                State = databaseModel.State,
                Major = databaseModel.Major,
                Degree = databaseModel.Degree,
                StartDate = databaseModel.StartDate,
                EndDate = databaseModel.EndDate
            };
        }

        public static IEnumerable<SchoolDomainModel> MapFrom(IEnumerable<School> databaseModels)
        {
            foreach (var databaseModel in databaseModels)
            {
                yield return MapFrom(databaseModel);
            }
        }
    }
}