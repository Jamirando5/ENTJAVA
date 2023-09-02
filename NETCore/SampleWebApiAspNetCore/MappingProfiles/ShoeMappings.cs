using AutoMapper;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.MappingProfiles
{
    public class ShoeMappings : Profile
    {
        public ShoeMappings()
        {
            CreateMap<ShoeEntity, ShoeDto>().ReverseMap();
            CreateMap<ShoeEntity, ShoeUpdateDto>().ReverseMap();
            CreateMap<ShoeEntity, ShoeCreateDto>().ReverseMap();
        }
    }
}
