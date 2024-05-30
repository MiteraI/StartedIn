using AutoMapper;
using CrossCutting.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;

namespace StartedIn.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [Authorize]
        [HttpGet("get-profile-info")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var username = HttpContext.User.Identity!.Name;
            var queryUser = await _userService.GetUserWithUserRolesByName(username);
            if (queryUser == null)
            {
                return BadRequest("Không tìm thấy người dùng!");
            }
            var profileDto = _mapper.Map<ProfileDTO>(queryUser);
            return Ok(profileDto);
        }

    }
}