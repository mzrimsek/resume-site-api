using System;
using AutoMapper;
using Core.Helpers;
using Core.Models;
using Web.Models.SchoolModels;

namespace Web.MapperProfiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<SchoolViewModel, SchoolDomainModel>()
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.StartDate)))
                .ForMember(dest => dest.EndDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.EndDate)));

            CreateMap<AddSchoolViewModel, SchoolDomainModel>()
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.StartDate)))
                .ForMember(dest => dest.EndDate,
                           opt => opt.MapFrom(src => DateTime.Parse(src.EndDate)));

            CreateMap<SchoolDomainModel, SchoolViewModel>()
                .ForMember(dest => dest.StartDate,
                           opt => opt.MapFrom(src => src.StartDate.Format()))
                .ForMember(dest => dest.EndDate,
                           opt => opt.MapFrom(src => src.EndDate.Format()));

            CreateMap<UpdateSchoolViewModel, SchoolViewModel>();
        }
    }
}