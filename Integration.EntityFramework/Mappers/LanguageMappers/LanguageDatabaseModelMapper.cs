using System.Collections.Generic;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.Mappers.LanguageMappers
{
    public static class LanguageDatabaseModelMapper
    {
        public static LanguageDatabaseModel MapFrom(LanguageDomainModel domainModel)
        {
            return new LanguageDatabaseModel()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Rating = domainModel.Rating
            };
        }

        public static IEnumerable<LanguageDatabaseModel> MapFrom(IEnumerable<LanguageDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }
    }
}