using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interface;
using Services.Exceptions;

namespace StartedIn.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ILogger<PostController> _logger;
        private readonly IUserService _userService;
        public PostController(IPostService postService, IMapper mapper, ILogger<PostController> logger, IUserService userService)
        {
            _postService = postService;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        [Route("/api/posts")]
        public async Task<ActionResult<List<PostResponseDTO>>> GetAllPost([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var postEntitiesList = await _postService.GetPostsAsync(pageIndex, pageSize);
                var responsePostList = _mapper.Map<List<PostResponseDTO>>(postEntitiesList);
                return Ok(responsePostList);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi server");
            }
        }
        [HttpPost]
        [Authorize]
        [Route("/api/posts/post")]
        public async Task<IActionResult> CreateNewPost([FromForm] PostRequestDTO postRequestDTO) 
        {
            try
            {
                var username = HttpContext.User.Identity!.Name;
                var queryUser = await _userService.GetUserWithUserRolesByName(username);
                if (queryUser == null)
                {
                    return BadRequest("Không tìm thấy người dùng!");
                }
                var newPost = _mapper.Map<Post>(postRequestDTO);
                await _postService.CreateNewPost(queryUser.Id, newPost);
                return StatusCode(201,"Tạo bài viết thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
