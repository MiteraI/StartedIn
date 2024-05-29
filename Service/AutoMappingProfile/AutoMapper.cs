using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.AutoMappingProfile
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {
            MapRegister();
        }
        private void MapRegister()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
        }
        
    }
}
