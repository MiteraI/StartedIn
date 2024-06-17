using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Service.Services.Interface
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Login(string email, string password);

        Task Register(User user, string password);

        Task<string> Refresh(string refreshToken);

        Task Revoke(string userId);
        Task ActivateUser(string userId);
        Task<User> GetUserByUserName(string name);

        Task<User> GetUserWithUserRolesById(string userId);

        Task<User> UpdateAvatar(IFormFile avatar, string userId);

        Task<User> UpdateProfile(User userToUpdate, string userId);

        Task<User> UpdateCoverPhoto(IFormFile coverPhoto, string userId);

        Task<User> GetUserWithId(string id);
    }
}
