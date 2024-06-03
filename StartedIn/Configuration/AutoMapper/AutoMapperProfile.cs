using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Service.AutoMappingProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            UserMappingProfile();
            PostMappingProfile();
        }

        private void UserMappingProfile() {
            CreateMap<User, RegisterDTO>().ReverseMap();
            CreateMap<User, HeaderProfileDTO>()
                .ForMember(userDto => userDto.UserRoles,
                    opt => opt.MapFrom(user
                        => user.UserRoles.Select(iur => iur.Role.Name).ToHashSet()))
                .ReverseMap()
                .ForPath(user => user.UserRoles, opt
                    => opt.MapFrom(userDto => userDto.UserRoles.Select(role => new UserRole { Role = new Role { Name = role } }).ToHashSet()));
            CreateMap<User, FullProfileDTO>().ReverseMap();
            CreateMap<User, UpdateProfileDTO>().ReverseMap();
        }
        private void PostMappingProfile() {
            CreateMap<Post, PostResponseDTO>().
                ForMember(postResponse => postResponse.ImgUrls,
                opt => opt.MapFrom(post => post.PostImages.Select(pi => pi.ImageLink).ToHashSet()))
                .ForMember(postResponse => postResponse.UserFullName,
                opt => opt.MapFrom(post => post.User.FullName))
                .ForMember(postResponse => postResponse.UserImgUrl,
                opt => opt.MapFrom(post => post.User.ProfilePicture))
                .ReverseMap();
            CreateMap<PostRequestDTO, Post>().ReverseMap();
        }
    }
}
