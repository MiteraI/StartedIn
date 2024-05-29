using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;
using System.Security.Claims;
namespace Service.Services.Interface
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Login(string email, string password);

        Task Register(User user, string password);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);

        Task<LoginResponseDTO> Refresh(RefreshTokenDTO refreshTokenDto);

        Task Revoke(string userName);
        Task ActivateUser(string userId);
        Task<User> GetUserByUserName(string name);
    }
}
