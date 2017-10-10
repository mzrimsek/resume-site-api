using AutoMapper;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.MapperProfiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobDatabaseModel, JobDomainModel>();
            CreateMap<JobDomainModel, JobDatabaseModel>();
            CreateMap<JobDatabaseModel, JobDatabaseModel>();
        }
    }
}