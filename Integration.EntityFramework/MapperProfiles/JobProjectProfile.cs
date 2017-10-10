using AutoMapper;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.MapperProfiles
{
    public class JobProjectProfile : Profile
    {
        public JobProjectProfile()
        {
            CreateMap<JobProjectDatabaseModel, JobProjectDomainModel>();
            CreateMap<JobProjectDomainModel, JobProjectDatabaseModel>();
            CreateMap<JobProjectDatabaseModel, JobProjectDatabaseModel>();
        }
    }
}