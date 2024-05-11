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
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController (IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto) 
        {
            ResponseDTO<string> res = await _accountService.Login(loginDto);
            if (res.statusCode == 200)
            {
                return Ok(res.message);
            }
            return StatusCode(res.statusCode, res.message);
        }
    }
}
