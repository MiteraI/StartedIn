﻿using AutoMapper;
using DataAccessLayer.DTOs.RequestDTO;
using DataAccessLayer.DTOs.ResponseDTO;
using DataAccessLayer.Enum;
using DataAccessLayer.Models;
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
using Role = DataAccessLayer.Enum.Role;

namespace Services.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService,
            UserManager<User> userManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _configuration = configuration;
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
            var user = _userRepository.GetById(loginUser.Id).Result;
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTimeOffset.UtcNow.AddHours(3);
            await _userRepository.Update(user);
            return new LoginResponseDTO<string>
            {
                StatusCode = 200,
                Message = "Login successful",
                JwtToken = jwtToken,
                RefreshToken = refreshToken,
            };
        }


        public async Task<ResponseDTO<string>> Register(RegisterDTO registerDto)
        {
            //_unitOfWork.BeginTransaction();
            //var user = new User
            //{
            //    UserName = registerDto.UserName,
            //    Email = registerDto.Email,
            //};
            //var result = await _userManager.CreateAsync(user, registerDto.Password);
            //switch (registerDto.Role)
            //{
            //    case Role.Admin:
            //        await _userManager.AddToRoleAsync(user, "Admin");
            //        break;

            //    case Role.User:
            //        await _userManager.AddToRoleAsync(user, "User");
            //        break;
            //}
            //if (result.Succeeded)
            //{
            //    var User = new User
            //    {
            //        Id = user.Id,
            //        NormalizedUserName = registerDto.Name,
            //        NormalizedEmail = registerDto.Email,
            //        Email = registerDto.Email,
            //        Status = AccountStatus.Active,
            //    };
            //    await _accountRepository.Add(user);
            //    await _unitOfWork.CommitAsync();
            //    return new ResponseDTO<string>
            //    {
            //        StatusCode = 200,
            //        Message = "Register successful",
            //    };

            //}
            //else
            //{
            //    await _unitOfWork.RollbackAsync();
            //    await _userManager.DeleteAsync(user);
            //    return new ResponseDTO<string>
            //    {
            //        StatusCode = 400,
            //        Message = "Register failed",
            //    };

            //}

            var existUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existUser != null)
            {
                return new ResponseDTO<string>
                {
                    StatusCode = 403,
                    Message = "User Already existed",
                };
            }

            User user = new()
            {
                Email = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDto.UserName
            };
            if (await _roleManager.RoleExistsAsync(registerDto.Role)) 
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (!result.Succeeded)
                {
                    return new ResponseDTO<string>
                    {
                        StatusCode = 500,
                        Message = "Created Failed",
                    };
                }
                await _userManager.AddToRoleAsync(user, registerDto.Role);
                return new ResponseDTO<string>
                {
                    StatusCode = 200,
                    Message = "Created Successfully",
                };
            }
            else
            {
                return new ResponseDTO<string>
                {
                    StatusCode = 500,
                    Message = "This role doesn't exist",
                };
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

        public async Task<LoginResponseDTO<string>> Refresh(RefreshTokenDTO refreshTokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(refreshTokenDto.JwtToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            var jwtToken = _tokenService.CreateTokenForAccount(user);
            return new LoginResponseDTO<string>
            {
                StatusCode = 200,
                Message = "Refresh successful",
                JwtToken = jwtToken,
                RefreshToken = refreshTokenDto.RefreshToken
            };
            
        }

        public async Task<ResponseDTO<string>> Revoke(string userName)
        {
            if (userName == null)
            {
                return new ResponseDTO<string>()
                {
                    StatusCode = 401,
                    Message = "Unauthorized"
                };
            }
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new ResponseDTO<string>()
                {
                    StatusCode = 401,
                    Message = "Unauthorized"
                };
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            await _userManager.UpdateAsync(user);
            return new ResponseDTO<string>()
            {
                StatusCode = 200,
                Message = "Revoke successful"
            };
        }
    }
}