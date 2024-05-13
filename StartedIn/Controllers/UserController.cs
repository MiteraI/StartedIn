using DataAccessLayer.DTOs.RequestDTO;
using DataAccessLayer.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(IUserService accountService, ITokenService tokenService)
        {
            _userService = accountService;
            _tokenService = tokenService;
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

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        //{
        //    var res = await _userService.Register(registerDto);
        //    if (res.StatusCode == 200)
        //    {
        //        return Ok("Register successful");
        //    }
        //    return StatusCode(res.StatusCode, res.Message);
        //}
    }
}