using Domain.DTOs.RequestDTO;
using Domain.DTOs.ResponseDTO;
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
        Task<LoginResponseDTO<string>> Login(LoginDTO loginDto);

        Task<ResponseDTO<string>> Register(RegisterDTO registerDto);

        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        
        Task<LoginResponseDTO<string>> Refresh(RefreshTokenDTO refreshTokenDto);

        Task<ResponseDTO<string>> Revoke(string userName);
    }
}
