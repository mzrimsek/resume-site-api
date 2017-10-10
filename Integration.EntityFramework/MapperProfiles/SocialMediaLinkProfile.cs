using AutoMapper;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.MapperProfiles
{
    public class SocialMediaLinkProfile : Profile
    {
        public SocialMediaLinkProfile()
        {
            CreateMap<SocialMediaLinkDatabaseModel, SocialMediaLinkDomainModel>();
            CreateMap<SocialMediaLinkDomainModel, SocialMediaLinkDatabaseModel>();
            CreateMap<SocialMediaLinkDatabaseModel, SocialMediaLinkDatabaseModel>();
        }
    }
}