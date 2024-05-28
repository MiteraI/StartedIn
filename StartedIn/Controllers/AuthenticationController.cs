using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs.RequestDTO;
using Domain.DTOs.ResponseDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interface;
using Services.Exceptions;
using Services.Interface;

namespace StartedIn.Controllers
{
    [ApiController]
    [Route("api")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;


        public AccountController(IUserService accountService, ITokenService tokenService,
            UserManager<User> userManager, IEmailService emailService)
        {
            _userService = accountService;
            _tokenService = tokenService;
            _userManager = userManager;
            _emailService = emailService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                LoginResponseDTO res = await _userService.Login(loginDto);
                return Ok(res);
            }
            catch (InvalidLoginException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotActivateException ex) 
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi đăng nhập xảy ra");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            try
            {
                await _userService.Register(registerDto);
                return Ok("Tạo tài khoản thành công");
            }
            catch (ExistedEmailException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi tạo tài khoản");
            }
            
            
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Refresh([FromBody] RefreshTokenDTO refreshTokenDto)
        {
            var principal = _userService.GetPrincipalFromExpiredToken(refreshTokenDto.JwtToken);
            
            if (principal?.Identity?.Name == null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetUserByUserName(principal.Identity.Name);

            if (user == null || user.RefreshToken != refreshTokenDto.RefreshToken ||
                user.RefreshTokenExpiry <= DateTimeOffset.UtcNow)
            {
                return Unauthorized();
            }

            var res = await _userService.Refresh(refreshTokenDto);


            return Ok(res);

        }

        [Authorize]
        [HttpDelete("revoke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Revoke()
        {
            try 
            {
                var userName = HttpContext.User.Identity.Name;
                if (userName == null)
                {
                    return Unauthorized();
                }
                await _userService.Revoke(userName);
                return Ok("Revoke Successfully !");
            }
            
            catch (Exception ex)
            {
                return BadRequest("Invalid refresh token");
            }
        
            
        }
        [HttpGet("activate-user/{userId}")]
        public async Task<IActionResult> ActivateUser(string userId)
        {
            
            try {
                await _userService.ActivateUser(userId);
                return Ok("Kích hoạt thành công!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi kích hoạt");
            }
        }
        
    }
}