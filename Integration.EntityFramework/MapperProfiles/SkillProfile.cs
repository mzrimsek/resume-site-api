using AutoMapper;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.MapperProfiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillDatabaseModel, SkillDomainModel>();
            CreateMap<SkillDomainModel, SkillDatabaseModel>();
            CreateMap<SkillDatabaseModel, SkillDatabaseModel>();
        }
    }
}