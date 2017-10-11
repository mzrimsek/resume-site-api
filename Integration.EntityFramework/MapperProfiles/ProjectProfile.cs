using AutoMapper;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.MapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectDatabaseModel, ProjectDomainModel>();
            CreateMap<ProjectDomainModel, ProjectDatabaseModel>();
            CreateMap<ProjectDatabaseModel, ProjectDatabaseModel>();
        }
    }
}