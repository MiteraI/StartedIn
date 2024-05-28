using AutoMapper;
using Domain.DTOs.RequestDTO;
using Domain.Entities;

namespace Services.AutoMappingProfile
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
