using AutoMapper;
using Core.Models;
using Web.Models.JobProjectModels;

namespace Web.MapperProfiles
{
    public class JobProjectProfile : Profile
    {
        public JobProjectProfile()
        {
            CreateMap<JobProjectViewModel, JobProjectDomainModel>();
            CreateMap<AddJobProjectViewModel, JobProjectDomainModel>();
            CreateMap<JobProjectDomainModel, JobProjectViewModel>();
            CreateMap<UpdateJobProjectViewModel, JobProjectViewModel>();
        }
    }
}