using System.Security.Claims;
using AutoMapper;
using CrossCutting.DTOs.ResponseDTO;
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
                return StatusCode(201, "Gửi lời mời kết nối thành công");
            }
            catch (Exception ex)
            {
                return BadRequest("Gửi lời mời kết nối thất bại");
            }
        }

        [Authorize]
        [HttpPut("connect/{connectionId}")]
        public async Task<IActionResult> AcceptConnection(string connectionId)
        {
        try
        {
            await _connectionService.AcceptConnection(connectionId);
            return StatusCode(200, "Kết nối thành công");
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest("Kết nối thất bại");
        }
        }

        [Authorize]
        [HttpGet("connect/pending-connection-receiving-request")]
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
    [Authorize]
    [HttpGet("connect/pending-connection-sending-request")]
    public async Task<IActionResult> GetConnectionSendingRequest([FromQuery] int pageIndex, int pageSize)
    {
        var senderId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        try
        {
            var connections = await _connectionService.GetUserConnectionSendingRequest(pageIndex, pageSize, senderId);
            var response = _mapper.Map<List<PendingSendingRequestDTO>>(connections);
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
    [Authorize]
    [HttpGet("connect/user-connection-list")]
    public async Task<IActionResult> GetUserConnectionList([FromQuery] int pageIndex, int pageSize)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        try
        {
            var connections = await _connectionService.GetUserConnectionList(pageIndex, pageSize, userId);

            var response = connections.Select(connection =>
            {
                var connectedUser = connection.SenderId == userId ? connection.Receiver : connection.Sender;
                var connectionDto = _mapper.Map<ConnectionDTO>(connectedUser);
                connectionDto.Id = connection.Id;
                return connectionDto;
            }).ToList();

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
    [Authorize]
    [HttpDelete("connect/{connectionId}")]
    public async Task<IActionResult> DeleteConnection(string connectionId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        try
        {
            await _connectionService.CancelConnection(connectionId);
            return Ok("Xoá kết nối thành công.");
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
