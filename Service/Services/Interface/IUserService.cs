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

        Task Revoke(string username);
        Task ActivateUser(string userId);
        Task<User> GetUserByUserName(string name);

        Task<User> GetUserWithUserRolesByName(string name);

        Task<User> UpdateAvatar(IFormFile avatar, string username);

        Task<User> UpdateProfile(User userToUpdate, string username);
    }
}
