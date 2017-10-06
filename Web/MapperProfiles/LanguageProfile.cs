using AutoMapper;
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
            CreateMap<LanguageDomainModel, LanguageViewModel>();
            CreateMap<UpdateLanguageViewModel, LanguageViewModel>();

        }
    }
}