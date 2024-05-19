using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs.RequestDTO;
using Domain.DTOs.ResponseDTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interface;
using Services.Interface;

namespace StartedIn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;

        public AccountController(IUserService accountService, ITokenService tokenService,
            UserManager<User> userManager)
        {
            _userService = accountService;
            _tokenService = tokenService;
            _userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            LoginResponseDTO<string> res = await _userService.Login(loginDto);
            if (res.StatusCode == 200)
            {
                return Ok(res);
            }
            return StatusCode(res.StatusCode, res.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var res = await _userService.Register(registerDto);
            if (res.StatusCode == 200)
            {
                return Ok("Register successful");
            }
            return StatusCode(res.StatusCode, res.Message);
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

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user == null || user.RefreshToken != refreshTokenDto.RefreshToken ||
                user.RefreshTokenExpiry <= DateTimeOffset.UtcNow)
            {
                return Unauthorized();
            }

            var res = await _userService.Refresh(refreshTokenDto);
            if (res.StatusCode == 200)
            {
                return Ok(res);
            }

            return BadRequest("Invalid token");

        }

        [Authorize]
        [HttpDelete("revoke")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Revoke()
        {
            var userName = HttpContext.User.Identity.Name;
            if (userName == null)
            {
                return Unauthorized();
            }
        
            var res = await _userService.Revoke(userName);
            if (res.StatusCode == 200)
            {
                return Ok(res);
            }
        
            return BadRequest("Invalid refresh token");
        }
        
    }
}