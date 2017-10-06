using System.Linq;
using AutoMapper;
using Core.Enums;
using Core.Models;
using Web.Models.LanguageModels;

namespace Web.MapperProfiles
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<LanguageViewModel, LanguageDomainModel>();
            CreateMap<AddLanguageViewModel, LanguageDomainModel>();

            CreateMap<LanguageDomainModel, LanguageViewModel>()
                .ForMember(dest => dest.RatingName,
                           opt => opt.MapFrom(src => RatingEnum.GetAll().SingleOrDefault(x => x.Key == src.Rating).Display));

            CreateMap<UpdateLanguageViewModel, LanguageViewModel>()
                .ForMember(dest => dest.RatingName,
                           opt => opt.MapFrom(src => RatingEnum.GetAll().SingleOrDefault(x => x.Key == src.Rating).Display));

        }
    }
}