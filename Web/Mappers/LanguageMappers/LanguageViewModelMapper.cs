using System.Collections.Generic;
using System.Linq;
using Core.Enums;
using Core.Models;
using Web.Models.LanguageModels;

namespace Web.Mappers.LanguageMappers
{
    public static class LanguageViewModelMapper
    {
        public static LanguageViewModel MapFrom(LanguageDomainModel domainModel)
        {
            var ratingName = RatingEnum.GetAll().SingleOrDefault(x => x.Key == domainModel.Rating).Display;
            return new LanguageViewModel()
            {
                Id = domainModel.Id,
                Name = domainModel.Name,
                Rating = domainModel.Rating,
                RatingName = ratingName
            };
        }

        public static IEnumerable<LanguageViewModel> MapFrom(IEnumerable<LanguageDomainModel> domainModels)
        {
            foreach (var domainModel in domainModels)
            {
                yield return MapFrom(domainModel);
            }
        }

        public static LanguageViewModel MapFrom(int id, AddUpdateLanguageViewModel viewModel)
        {
            var ratingName = RatingEnum.GetAll().SingleOrDefault(x => x.Key == viewModel.Rating).Display;
            return new LanguageViewModel()
            {
                Id = id,
                Name = viewModel.Name,
                Rating = viewModel.Rating,
                RatingName = ratingName
            };
        }
    }
}