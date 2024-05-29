using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Exceptions;
using Domain.Entities;
using Service.Services.Interface;
using Repository.Repositories.Interface;
using CrossCutting.DTOs.ResponseDTO;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.Exceptions;
using CrossCutting.Constants;
using Microsoft.Extensions.Logging;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;

        public UserService(ITokenService tokenService,
            UserManager<User> userManager, IUnitOfWork unitOfWork,
            IConfiguration configuration, IEmailService emailService,
            ILogger<UserService> logger
            )
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<LoginResponseDTO> Login(string email, string password)
        {
            var loginUser = await _userManager.FindByEmailAsync(email);
            if (loginUser == null || !await _userManager.CheckPasswordAsync(loginUser, password))
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


        public async Task Register(User registerUser, string password)
        {
            var existUser = await _userManager.FindByEmailAsync(registerUser.Email);
            if (existUser != null)
            {
                throw new ExistedEmailException("Email này đã tồn tại.");
            }
            
            try {
                _unitOfWork.BeginTransaction();
                registerUser.UserName = registerUser.Email;
                var result = await _userManager.CreateAsync(registerUser, password);
                if (!result.Succeeded)
                {
                    throw new InvalidRegisterException("Đăng ký thất bại");
                }
                await _userManager.AddToRoleAsync(registerUser, RoleConstants.USER);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                // Only send mail if user is created successfully
                _emailService.SendVerificationMail(registerUser.Email, registerUser.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while register");
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