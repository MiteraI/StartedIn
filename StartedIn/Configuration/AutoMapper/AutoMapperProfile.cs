using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.AutoMappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
        }
    }
}
