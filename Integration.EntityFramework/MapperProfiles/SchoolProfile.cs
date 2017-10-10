using AutoMapper;
using Core.Models;
using Integration.EntityFramework.Models;

namespace Integration.EntityFramework.MapperProfiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<SchoolDatabaseModel, SchoolDomainModel>();
            CreateMap<SchoolDomainModel, SchoolDatabaseModel>();
            CreateMap<SchoolDatabaseModel, SchoolDatabaseModel>();
        }
    }
}