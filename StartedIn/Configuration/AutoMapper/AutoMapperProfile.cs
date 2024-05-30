using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;

namespace Service.AutoMappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, ProfileDTO>()
                .ForMember(userDto => userDto.UserRoles, 
                    opt => opt.MapFrom(user 
                        => user.UserRoles.Select(iur => iur.Role.Name).ToHashSet()))
                .ReverseMap()
                .ForPath(user => user.UserRoles, opt 
                    => opt.MapFrom(userDto => userDto.UserRoles.Select(role => new UserRole { Role = new Role { Name = role }}).ToHashSet()));
        }
    }
}
