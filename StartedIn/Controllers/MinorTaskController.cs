using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using Services.Exceptions;

namespace StartedIn.Controllers
{
    [ApiController]
    [Route("api")]
    public class MinorTaskController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MinorTaskController> _logger;
        private readonly IMinorTaskService _minorTaskService;

        public MinorTaskController(IMapper mapper, ILogger<MinorTaskController> logger, IMinorTaskService minorTaskService)
        {
            _minorTaskService = minorTaskService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("minortask/create")]
        public async Task<ActionResult<MinorTaskResponseDTO>> CreateNewMinorTask(MinorTaskCreateDTO minorTaskCreateDto)
        {
            try
            {
                var minorTask = await _minorTaskService.CreateMinorTask(minorTaskCreateDto);
                var response = _mapper.Map<MinorTaskResponseDTO>(minorTask);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new minor task.");
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpPut("minortask/move")]
        public async Task<ActionResult<MinorTaskResponseDTO>> MoveMinorTask(UpdateMinorTaskPositionDTO updateMinorTaskPositionDTO)
        {
            try
            {
                var responseMinorTask = _mapper.Map<MinorTaskResponseDTO>(await _minorTaskService.MoveMinorTask(updateMinorTaskPositionDTO.Id, updateMinorTaskPositionDTO.TaskboardId, updateMinorTaskPositionDTO.Position, updateMinorTaskPositionDTO.NeedsReposition));
                return Ok(responseMinorTask);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Di chuyển Task nhỏ thất bại");
            }
        }

        [HttpPut("minortask/edit/{id}")]
        public async Task<ActionResult<MinorTaskResponseDTO>> EditInfoMinorTask(string id, [FromBody] UpdateMinorTaskInfoDTO updateMinorTaskInfoDTO)
        {
            try
            {
                var responseMinorTask = _mapper.Map<MinorTaskResponseDTO>(await _minorTaskService.UpdateMinorTaskInfo(id, updateMinorTaskInfoDTO));
                return Ok(responseMinorTask);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Cập nhật thất bại");
            }
        }

        [HttpGet("minortask/{id}/get-assignable-major-tasks")]
        public async Task<ActionResult<IEnumerable<AssignableMajorTaskResponseDTO>>> GetAssignableMajorTasks(string id)
        {
            try
            {
                var response = _mapper.Map<IEnumerable<AssignableMajorTaskResponseDTO>>(await _minorTaskService.GetAssignableMajorTasks(id));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Truy xuất dữ liệu thất bại");
            }
        }
    }
}
