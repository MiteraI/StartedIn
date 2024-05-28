using Domain.DTOs.RequestDTO;
using Domain.DTOs.ResponseDTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IUserService
    {
        Task<LoginResponseDTO> Login(LoginDTO loginDto);

        Task Register(RegisterDTO registerDto);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        
        Task<LoginResponseDTO> Refresh(RefreshTokenDTO refreshTokenDto);

        Task Revoke(string userName);
        Task ActivateUser(string userId);
        Task<User> GetUserByUserName(string name);
    }
}
