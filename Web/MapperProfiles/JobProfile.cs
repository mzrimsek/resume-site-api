using System;
using AutoMapper;
using Core.Helpers;
using Core.Models;
using Web.Models.JobModels;

namespace Web.MapperProfiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobViewModel, JobDomainModel>()
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.StartDate)))
                .ForMember(dest => dest.EndDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.EndDate)));

            CreateMap<AddJobViewModel, JobDomainModel>()
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.StartDate)))
                .ForMember(dest => dest.EndDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.EndDate)));

            CreateMap<JobDomainModel, JobViewModel>()
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => src.StartDate.Format()))
                .ForMember(dest => dest.EndDate,
                           opt => opt.MapFrom(src => src.EndDate.Format()));

            CreateMap<AddJobViewModel, JobViewModel>();
            CreateMap<UpdateJobViewModel, JobViewModel>();
        }
    }
}