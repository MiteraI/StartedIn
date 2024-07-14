using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using CrossCutting.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using Services.Exceptions;

namespace StartedIn.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;
        private readonly IMapper _mapper;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger, IMapper mapper)
        {
            _projectService = projectService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("team/{id}/projects")]
        public async Task<ActionResult<List<ProjectResponseDTO>>> GetProjectsByTeamId(string id)
        {
            try
            {
                var projects = await _projectService.GetProjectsByTeamId(id);
                var response = _mapper.Map<List<ProjectResponseDTO>>(projects);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting project list.");
                return StatusCode(500,"Lỗi server");
            }
        }

        [HttpPost("project/create")]
        public async Task<ActionResult<ProjectResponseDTO>> CreateNewProject(NewProjectCreateDTO projectCreateDto)
        {
            try
            {
                var project = await _projectService.CreateNewProject(projectCreateDto);
                var response = _mapper.Map<ProjectResponseDTO>(project);
                return Ok(response);
            }
            catch (ExistedRecordException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new project.");
                return StatusCode(500,"Lỗi server");
            }
        }

        [HttpGet("projects/{id}")]
        public async Task<ActionResult<ProjectResponseDTO>> GetProjectById(string id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);
                var mappedProject = _mapper.Map<ProjectResponseDTO>(project);
                return Ok(mappedProject);
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
    }
}
