using System.Security.Claims;
using AutoMapper;
using CrossCutting.DTOs.ResponseDTO;
using CrossCutting.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using Services.Exceptions;

namespace StartedIn.Controllers;

    [ApiController]
    [Route("api")]
    public class ConnectionController : ControllerBase
    {
        private readonly IConnectionService _connectionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ConnectionController(IConnectionService connectionService, IUserService userService, IMapper mapper)
        {
            _connectionService = connectionService;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("connect/{receiverId}")]
        public async Task<IActionResult> CreateConnection(string receiverId)
        {
            try
            {
                var senderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _connectionService.CreateConnection(senderId, receiverId);
                return StatusCode(201, "Tạo connection thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Tạo connection thất bại");
            }
        }

        [Authorize]
        [HttpPost("connect/{connectionId}/{responseId}")]
        public async Task<IActionResult> RespondConnection(string connectionId, int responseId)
        {
            try
            {
                await RespondConnection(connectionId, responseId);
                return StatusCode(201, "Respond connection thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Respond connection thất bại");
            }
        }

        [Authorize]
        [HttpGet("connections")]
        public async Task<IActionResult> GetPendingConnections([FromQuery] int pageIndex, int pageSize)
        {
            var receiverId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            try
            {
                var connections = await _connectionService.GetPendingConnections(pageIndex, pageSize, receiverId);
                var response = _mapper.Map<List<PendingConnectionDTO>>(connections);
                return Ok(response);
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