using System.Collections.Generic;
using Core.Models;
using Web.Models.LanguageModels;

namespace Web.Mappers.LanguageMappers
{
    public static class LanguageDomainModelMapper
    {
        public static LanguageDomainModel MapFrom(LanguageViewModel viewModel)
        {
            return new LanguageDomainModel()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Rating = viewModel.Rating
            };
        }

        public static IEnumerable<LanguageDomainModel> MapFrom(IEnumerable<LanguageViewModel> viewModels)
        {
            foreach (var viewModel in viewModels)
            {
                yield return MapFrom(viewModel);
            }
        }

        public static LanguageDomainModel MapFrom(AddLanguageViewModel viewModel)
        {
            return new LanguageDomainModel()
            {
                Name = viewModel.Name,
                Rating = viewModel.Rating
            };
        }
    }
}