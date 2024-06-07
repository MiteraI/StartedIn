using AutoMapper;
using CrossCutting.Constants;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using Services.Exceptions;
using System.Security.Claims;

namespace StartedIn.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly ILogger<PostController> _logger;
        private readonly IUserService _userService;
        private readonly IAzureBlobService _azureBlobService;
        public PostController(IPostService postService, IMapper mapper, ILogger<PostController> logger, IUserService userService, IAzureBlobService azureBlobService)
        {
            _postService = postService;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
            _azureBlobService = azureBlobService;
        }
        [HttpGet]
        [Authorize(Roles = RoleConstants.ADMIN)]
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
        [HttpGet]
        [Authorize(Roles = RoleConstants.USER)]
        [Route("/api/posts/active-posts")]
        public async Task<ActionResult<List<PostResponseDTO>>> GetAllActivePost([FromQuery] int pageIndex, [FromQuery] int pageSize)
        {
            try
            {
                var postEntitiesList = await _postService.GetActivePostAsync(pageIndex, pageSize);
                var responseActivePostList = _mapper.Map<List<PostResponseDTO>>(postEntitiesList);
                return Ok(responseActivePostList);
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
        [Route("/api/posts")]
        public async Task<ActionResult<PostResponseDTO>> CreateNewPost([FromForm] PostRequestDTO postRequestDTO) 
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var newPost = _mapper.Map<Post>(postRequestDTO);
                await _postService.CreateNewPost(userId, newPost,postRequestDTO.PostImageLinks);
                var responseNewPost = _mapper.Map<PostResponseDTO>(newPost);
                return CreatedAtAction(nameof(GetPostById), new { id = responseNewPost.Id }, responseNewPost);
            }
            catch (Exception ex)
            {
                return BadRequest("Tạo post thất bại.");
            }
        }
        [HttpGet]
        [Authorize]
        [Route("/api/posts/{id}")]
        public async Task<IActionResult> GetPostById([FromRoute] string id)
        {
            try
            {
                var chosenPost = _mapper.Map<PostResponseDTO>(await _postService.GetPostsById(id));
                return Ok(chosenPost);
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
    }
}
