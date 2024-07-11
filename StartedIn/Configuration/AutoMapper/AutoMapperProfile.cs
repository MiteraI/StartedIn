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
            UserMappingProfile();
            PostMappingProfile();
            TeamMappingProfile();
            ProjectMappingProfile();
            ConnectionMappingProfile();
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
        private void TeamMappingProfile() {
            CreateMap<Team, TeamCreateRequestDTO>().ReverseMap();
            CreateMap<Team, TeamResponseDTO>()
               .ForMember(tr => tr.Users,
                   opt => opt.MapFrom(t
                       => t.TeamUsers.Select(tu => tu.User.FullName).ToHashSet()))
               .ForMember(tr => tr.Projects, opt => opt.MapFrom(t=>t.Projects))
               .ReverseMap()
               .ForPath(t => t.TeamUsers, opt
                   => opt.MapFrom(tr => tr.Users.Select(name => new TeamUser { User = new User { FullName = name } }).ToHashSet()));

        }
        private void ProjectMappingProfile()
        {
            CreateMap<Project, ProjectCreateDTO>().ReverseMap();
            CreateMap<Project, ProjectResponseDTO>().ReverseMap();
        }

        private void ConnectionMappingProfile()
        {
            CreateMap<Connection, PendingConnectionDTO>()
                .ForMember(c => c.SenderName,
                    opt => opt.MapFrom(con => con.Sender.FullName))
                .ForMember(c => c.ProfilePicture,
                    opt => opt.MapFrom(con => con.Sender.ProfilePicture))
                .ForMember(c => c.Time,
                    opt => opt.MapFrom(con => con.CreatedTime))
                .ReverseMap();
            CreateMap<Connection, PendingSendingRequestDTO>()
                .ForMember(c => c.ReceiverName,
                    opt => opt.MapFrom(con => con.Receiver.FullName))
                .ForMember(c => c.ProfilePicture,
                    opt => opt.MapFrom(con => con.Receiver.ProfilePicture))
                .ForMember(c => c.Time,
                    opt => opt.MapFrom(con => con.CreatedTime))
                .ReverseMap();

            CreateMap<Connection, ConnectionDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.Ignore())  // We will handle this in the controller
            .ForMember(dest => dest.ConnectedUserName, opt => opt.Ignore())  // We will handle this in the controller
            .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());  // We will handle this in the controller

            CreateMap<User, ConnectionDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ConnectedUserName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

        }
    }
}
