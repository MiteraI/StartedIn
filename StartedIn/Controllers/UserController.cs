using AutoMapper;
using CrossCutting.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using Services.Exceptions;

namespace StartedIn.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _mapper = mapper;
            _userService = userService;
            _logger = logger;
        }
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<FullProfileDTO>>> GetUserLists([FromQuery] int pageIndex, int pageSize)
        {
            try
            {
                var userList = await _userService.GetUsersList(pageIndex, pageSize);
                var responseUserList = _mapper.Map<List<FullProfileDTO>>(userList);
                return responseUserList;
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "No user found.");
                return NotFound(ex);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while getting users list.");
                return StatusCode(500,"Lỗi server");
            }

        }
    }
}
