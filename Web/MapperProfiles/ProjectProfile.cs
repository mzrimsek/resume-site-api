using AutoMapper;
using Core.Models;
using Web.Models.ProjectModels;

namespace Web.MapperProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectViewModel, ProjectDomainModel>();
            CreateMap<AddProjectViewModel, ProjectDomainModel>();
            CreateMap<ProjectDomainModel, ProjectViewModel>();
            CreateMap<UpdateProjectViewModel, ProjectViewModel>();
        }
    }
}