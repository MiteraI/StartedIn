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

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet("team/{id}/projects")]
        public async Task<IActionResult> GetProjectsByTeamId(string id)
        {
            try
            {
                var projects = await _projectService.GetProjectsByTeamId(id);
                return Ok(projects);
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
        public async Task<IActionResult> CreateNewProject(NewProjectCreateDTO projectCreateDto)
        {
            try
            {
                await _projectService.CreateNewProject(projectCreateDto);
                return StatusCode(201, "Tạo project thành công");
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
    }
}
