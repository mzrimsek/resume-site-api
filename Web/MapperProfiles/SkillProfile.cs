using AutoMapper;
using System.Linq;
using Core.Enums;
using Core.Models;
using Web.Models.SkillModels;

namespace Web.MapperProfiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillViewModel, SkillDomainModel>();
            CreateMap<AddSkillViewModel, SkillDomainModel>();

            CreateMap<SkillDomainModel, SkillViewModel>()
                .ForMember(dest => dest.RatingName,
                           opt => opt.MapFrom(src => RatingEnum.GetAll().SingleOrDefault(x => x.Key == src.Rating).Display));

            CreateMap<UpdateSkillViewModel, SkillViewModel>()
                .ForMember(dest => dest.RatingName,
                           opt => opt.MapFrom(src => RatingEnum.GetAll().SingleOrDefault(x => x.Key == src.Rating).Display));
        }
    }
}