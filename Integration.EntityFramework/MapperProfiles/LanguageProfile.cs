using AutoMapper;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.MapperProfiles
{
    public class LanguageProfile : Profile
    {
        public LanguageProfile()
        {
            CreateMap<LanguageDatabaseModel, LanguageDomainModel>();
            CreateMap<LanguageDomainModel, LanguageDatabaseModel>();
        }
    }
}