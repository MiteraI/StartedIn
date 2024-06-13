using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        
        // Lấy các thông tin cần để hiển thị ở profile header
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var username = HttpContext.User.Identity!.Name;
            var queryUser = await _userService.GetUserWithUserRolesByName(username);
            if (queryUser == null)
            {
                return BadRequest("Không tìm thấy người dùng!");
            }
            var profileDto = _mapper.Map<HeaderProfileDTO>(queryUser);
            return Ok(profileDto);
        }
        
        // lấy full cho page profile
        [Authorize]
        [HttpGet("full-profile")]
        public async Task<IActionResult> GetCurrentUserFullProfile()
        {
            var username = HttpContext.User.Identity!.Name;
            var queryUser = await _userService.GetUserByUserName(username);
            if (queryUser == null)
            {
                return BadRequest("Không tìm thấy người dùng!");
            }

            var fullProfileDto = _mapper.Map<FullProfileDTO>(queryUser);
            return Ok(fullProfileDto);
        }
        
        [Authorize]
        [HttpPost("profile/avatar")]
        public async Task<IActionResult> UploadAvatar(IFormFile avatar)
        {
            var username = HttpContext.User.Identity!.Name;
            if (username == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }
            try
            {
                await _userService.UpdateAvatar(avatar, username);
                return Ok("Cập nhật ảnh đại diện thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Cập nhật ảnh đại diện thất bại");
            }
        }
        [Authorize]
        [HttpPost("profile/cover-photo")]
        public async Task<IActionResult> UploadCoverPhoto(IFormFile coverPhoto)
        {
            var username = HttpContext.User.Identity!.Name;
            if (username == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }
            try
            {
                await _userService.UpdateCoverPhoto(coverPhoto, username);
                return Ok("Cập nhật ảnh bìa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Cập nhật ảnh bìa thất bại");
            }
        }

        [Authorize]
        [HttpPut("profile/edit")]
        public async Task<IActionResult> EditProfile([FromBody]UpdateProfileDTO updateProfileDto)
        {
            var username = HttpContext.User.Identity!.Name;
            if (username == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }

            try
            {
                var user = await _userService.UpdateProfile(_mapper.Map<User>(updateProfileDto), username);
                var updatedUser = _mapper.Map<FullProfileDTO>(user);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest("Cập nhật người dùng không thành công");
            }
        }
        
        [HttpGet("users/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(string userId)
        {
            try
            {
                var user = await _userService.GetUserWithId(userId);
                return Ok(_mapper.Map<FullProfileDTO>(user));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server");
            }
        }
        

    }
}