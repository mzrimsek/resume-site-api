using AutoMapper;
using Core.Models;
using Web.Models.SocialMediaLinkModels;

namespace Web.MapperProfiles
{
    public class SocialMediaLinkProfile : Profile
    {
        public SocialMediaLinkProfile()
        {
            CreateMap<SocialMediaLinkViewModel, SocialMediaLinkDomainModel>();
            CreateMap<AddSocialMediaLinkViewModel, SocialMediaLinkDomainModel>();
            CreateMap<SocialMediaLinkDomainModel, SocialMediaLinkViewModel>();
            CreateMap<UpdateSocialMediaLinkViewModel, SocialMediaLinkViewModel>();
        }
    }
}