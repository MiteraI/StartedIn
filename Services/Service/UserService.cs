using AutoMapper;
using Domain.DTOs.RequestDTO;
using Domain.DTOs.ResponseDTO;
using Domain.Enum;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Repositories.Interface;
using Services.Interface;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Repository.Interface;
using Services.Exceptions;
using Domain.Entities;

namespace Services.Service
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public UserService(IMapper mapper, ITokenService tokenService,
            UserManager<User> userManager, IUnitOfWork unitOfWork, RoleManager<Role> roleManager,
            IConfiguration configuration, IEmailService emailService)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<LoginResponseDTO> Login(LoginDTO loginDto)
        {
            var loginUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (loginUser == null || !await _userManager.CheckPasswordAsync(loginUser, loginDto.Password))
            {
               throw new InvalidLoginException("Đăng nhập thất bại!");
            }
            if (loginUser.EmailConfirmed == false)
            {
                throw new NotActivateException("Tài khoản chưa được xác thực");
            }
            var jwtToken = _tokenService.CreateTokenForAccount(loginUser);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var user = await _userManager.FindByIdAsync(loginUser.Id);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddHours(3);
            await _userManager.UpdateAsync(user);
            return new LoginResponseDTO
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken,
            };
        }


        public async Task Register(RegisterDTO registerDto)
        {
            var existUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existUser != null)
            {
                throw new ExistedEmailException("Email này đã tồn tại.");
            }
            User user = new()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                FullName = registerDto.FullName,
                AccessFailedCount = 0
            };
            try {
                _unitOfWork.BeginTransaction();
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    throw new InvalidRegisterException("Đăng ký thất bại");
                }
                await _userManager.AddToRoleAsync(user, "User");
                _emailService.SendVerificationMail(registerDto.Email, user.Id);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }   
        }
        
        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var validation = new TokenValidationParameters
            {
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                    (_configuration["jwt:secret"])),
                ValidIssuer = _configuration["jwt:issuer"],
                ValidAudience = _configuration["jwt:audience"]
            };
            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        public async Task<LoginResponseDTO> Refresh(RefreshTokenDTO refreshTokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(refreshTokenDto.JwtToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            var jwtToken = _tokenService.CreateTokenForAccount(user);
            return new LoginResponseDTO
            {
                AccessToken = jwtToken,
                RefreshToken = refreshTokenDto.RefreshToken
            };
            
        }

        public async Task Revoke(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                throw new NotFoundException("Username is not found!");
            }
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
        }

        public async Task ActivateUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException($"Unable to activate user {userId}");
            }
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
        }
        public async Task<User> GetUserByUserName(string name) 
        {
            var user = await _userManager.FindByNameAsync(name);
            return user;
        }
    }
}