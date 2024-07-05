﻿using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using CrossCutting.Exceptions;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
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
                var newTeam = _mapper.Map<Team>(teamAndProjectCreateDTO.TeamCreateRequestDTO);
                var newProject = _mapper.Map<Project>(teamAndProjectCreateDTO.ProjectCreateDTO);
                await _teamService.CreateNewTeam(userId, newTeam, newProject);
                return StatusCode(201, "Tạo team thành công");
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



    }
}
