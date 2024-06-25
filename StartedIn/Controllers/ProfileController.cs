using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using System.Security.Claims;
using Services.Exceptions;

namespace StartedIn.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(IUserService userService, IMapper mapper, ILogger<ProfileController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }
        
        // Lấy các thông tin cần để hiển thị ở profile header
        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetCurrentUserProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var queryUser = await _userService.GetUserWithUserRolesById(userId);
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var queryUser = await _userService.GetUserWithId(userId);
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }
            try
            {
                await _userService.UpdateAvatar(avatar, userId);
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }
            try
            {
                await _userService.UpdateCoverPhoto(coverPhoto, userId);
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId == null)
            {
                return BadRequest("Không tìm thấy người dùng");
            }

            try
            {
                var user = await _userService.UpdateProfile(_mapper.Map<User>(updateProfileDto), userId);
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
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "No user found.");
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while getting user.");
                return StatusCode(500,"Lỗi server");
            }
        }
        

    }
}