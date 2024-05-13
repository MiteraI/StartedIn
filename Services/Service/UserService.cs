using AutoMapper;
using DataAccessLayer.DTOs.RequestDTO;
using DataAccessLayer.DTOs.ResponseDTO;
using DataAccessLayer.Enum;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Repositories.Interface;
using Services.Interface;
using System.Data;
using System.Security.Principal;

namespace Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserService(IUserRepository accountRepository, IMapper mapper, ITokenService tokenService,
            UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponseDTO<string>> Login(LoginDTO loginDto)
        {
            // var loginUser = await _accountRepository.Login(loginDto.Email ?? string.Empty, loginDto.Password ?? string.Empty);
            var loginUser = await _userManager.FindByEmailAsync(loginDto.Email);
            if (loginUser == null || !await _userManager.CheckPasswordAsync(loginUser, loginDto.Password))
            {
                return new LoginResponseDTO<string>
                {
                    StatusCode = 400,
                    Message = "Wrong email or password"
                };
            }
            var jwtToken = _tokenService.CreateTokenForAccount(loginUser);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var user = _accountRepository.GetById(loginUser.Id).Result;
            RefreshToken token = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddHours(3)
            };
            _refreshTokenRepository.Add(token);

            return new LoginResponseDTO<string>
            {
                StatusCode = 200,
                Message = "Login successful",
                JwtToken = jwtToken,
                RefreshToken = refreshToken,
            };
        }

        //public async Task<ResponseDTO<string>> Register(RegisterDTO registerDto)
        //{
        //    _unitOfWork.BeginTransaction();
        //    var user = new User
        //    {
        //        UserName = registerDto.UserName,
        //        Email = registerDto.Email,
        //    };
        //    var result = await _userManager.CreateAsync(user, registerDto.Password);
        //    switch (registerDto.Role)
        //    {
        //        case Role.Admin:
        //            await _userManager.AddToRoleAsync(user, "Admin");
        //            break;

        //        case Role.User:
        //            await _userManager.AddToRoleAsync(user, "User");
        //            break;
        //    }
        //    if (result.Succeeded)
        //    {
        //        var newAccount = new Account
        //        {
        //            Id = user.Id,
        //            Name = registerDto.Name,
        //            Email = registerDto.Email,
        //            Status = AccountStatus.Active,
        //        };
        //        await _accountRepository.Add(newAccount);
        //        await _unitOfWork.CommitAsync();
        //        return new ResponseDTO<string>
        //        {
        //            StatusCode = 200,
        //            Message = "Register successful",
        //        };

        //    }
        //    else
        //    {
        //        await _unitOfWork.RollbackAsync();
        //        await _userManager.DeleteAsync(user);
        //        return new ResponseDTO<string>
        //        {
        //            StatusCode = 400,
        //            Message = "Register failed",
        //        };

        //    }
        //}

    }
}