using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.LanguageMappers
{
    public static class LanguageDomainModelMapper
    {
        public static LanguageDomainModel MapFrom(LanguageDatabaseModel databaseModel)
        {
            return new LanguageDomainModel()
            {
                Id = databaseModel.Id,
                Name = databaseModel.Name,
                Rating = databaseModel.Rating
            };
        }

        public static IEnumerable<LanguageDomainModel> MapFrom(IEnumerable<LanguageDatabaseModel> databaseModels)
        {
            foreach (var databaseModel in databaseModels)
            {
                yield return MapFrom(databaseModel);
            }
        }
    }
}