using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using CrossCutting.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using Services.Exceptions;
using System.Security.Claims;

namespace StartedIn.Controllers
{
    [ApiController]
    [Route("api")]
    public class TeamController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TeamController> _logger;
        private readonly ITeamService _teamService;
        public TeamController(ITeamService teamService, ILogger<TeamController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _teamService = teamService;
        }
        [HttpPost("teams")]
        [Authorize]
        public async Task<IActionResult> CreateNewStartup(TeamAndProjectCreateDTO teamAndProjectCreateDTO) 
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var newTeam = _mapper.Map<Team>(teamAndProjectCreateDTO.Team);
                var newProject = _mapper.Map<Project>(teamAndProjectCreateDTO.Project);
                await _teamService.CreateNewTeam(userId, newTeam, newProject);
                var responseNewStartUp = _mapper.Map<TeamResponseDTO>(newTeam);
                return CreatedAtAction(nameof(GetTeamById), new { teamId = responseNewStartUp.Id }, responseNewStartUp);
            }
            catch (ExistedRecordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Tạo team thất bại.");
            }
        }
        
        [HttpGet("teams/user-team")]
        [Authorize]
        public async Task<ActionResult<TeamResponseDTO>> GetTeamByUserId()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var teamEntityList = await _teamService.GetTeamByUserId(userId);
                var responseTeamList = _mapper.Map<List<TeamResponseDTO>>(teamEntityList);
                return Ok(responseTeamList);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("teams/team-invitation/{teamId}")]
        [Authorize]
        public async Task<IActionResult> SendInvitationToTeam([FromBody] List<string> userIds, [FromRoute] string teamId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await _teamService.SendJoinTeamInvitation(userId, userIds, teamId);
                return Ok("Gửi lời mời gia nhập thành công");
            }
            catch (TeamLimitException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InviteException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("invite/add/{teamId}/{userId}")]
        [Authorize]
        public async Task<IActionResult> AddUserToTeam([FromRoute] string teamId, string userId)
        {
            try
            {
                await _teamService.AddUserToTeam(teamId,userId);
                return Ok("Bạn đã được tham gia nhóm!");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InviteException ex)
            {
                return BadRequest("Người dùng đã tồn tại trong team");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("teams/user-leader-team")]
        [Authorize]
        public async Task<ActionResult<TeamResponseDTO>> GetTeamByLeaderUserId()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var teamEntityList = await _teamService.GetTeamByUserIfLeader(userId);
                var responseTeamList = _mapper.Map<List<TeamResponseDTO>>(teamEntityList);
                return Ok(responseTeamList);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("teams/{teamId}/team-members")]
        [Authorize]
        public async Task<ActionResult<TeamWithMembersResponseDTO>> GetTeamByIdWithMember(string teamId)
        {
            try
            {
                var teamEntity = await _teamService.GetTeamById(teamId);
                var responseTeam = _mapper.Map<TeamWithMembersResponseDTO>(teamEntity);
                return Ok(responseTeam);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("teams/user-guest-team")]
        [Authorize]
        public async Task<ActionResult<TeamResponseDTO>> GetTeamByGuestUserId()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var teamEntityList = await _teamService.GetTeamByUserIfGuest(userId);
                var responseTeamList = _mapper.Map<List<TeamResponseDTO>>(teamEntityList);
                return Ok(responseTeamList);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("invite/view/{teamId}")]
        public async Task<ActionResult<TeamInvitationResponseDTO>> GetTeamDetailByIdWithLeader(string teamId)
        {
            try
            {
                var teamEntity = await _teamService.GetTeamById(teamId);
                var responseTeam = _mapper.Map<TeamInvitationResponseDTO>(teamEntity);
                return Ok(responseTeam);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("teams/{teamId}")]
        public async Task<ActionResult<TeamResponseDTO>> GetTeamById(string teamId) 
        {
            try
            {
                var teamEntity = await _teamService.GetTeamById(teamId);
                var responseTeam = _mapper.Map<TeamResponseDTO>(teamEntity);
                return Ok(responseTeam);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,"Lỗi server");
            }
        }





    }
}
