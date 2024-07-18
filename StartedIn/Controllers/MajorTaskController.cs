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
    public class MajorTaskController : ControllerBase
    {
        private readonly IMajorTaskService _majorTaskService;
        private readonly IMapper _mapper;
        private readonly ILogger<MajorTaskController> _logger;

        public MajorTaskController(IMajorTaskService majorTaskService, IMapper mapper, ILogger<MajorTaskController> logger)
        {
            _majorTaskService = majorTaskService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("majortask/create")]
        public async Task<ActionResult<MajorTaskResponseDTO>> CreateNewMajorTask(MajorTaskCreateDTO majorTaskCreateDto)
        {
            try
            {
                string id = await _majorTaskService.CreateNewMajorTask(majorTaskCreateDto);
                return StatusCode(201, new { message = "Tạo task thành công", id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new major task.");
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpPut("majortask/move")]
        public async Task<ActionResult<MajorTaskResponseDTO>> MoveMajorTask(UpdateMajorTaskPositionDTO updateMajorTaskPositionDTO)
        {
            try
            {
                var responseMajorTask = _mapper.Map<MajorTaskResponseDTO>(await _majorTaskService.MoveMajorTask(updateMajorTaskPositionDTO.Id, updateMajorTaskPositionDTO.PhaseId, updateMajorTaskPositionDTO.Position, updateMajorTaskPositionDTO.NeedsReposition));
                return Ok(responseMajorTask);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Di chuyển Task lớn thất bại");
            }
        }

        [HttpGet("majortask/{mjTaskId}")]
        public async Task<ActionResult<MajorTaskWithListMinorTasksResponseDTO>> GetMajorTaskById([FromRoute] string mjTaskId)
        {
            try
            {
                var responseMajorTask = _mapper.Map<MajorTaskWithListMinorTasksResponseDTO>(await _majorTaskService.GetMajorTaskById(mjTaskId));
                return Ok(responseMajorTask);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi Server");
            }
        }

        [HttpPut("majortask/edit/{id}")]
        public async Task<ActionResult<MajorTaskResponseDTO>> EditInfoMajorTask(string id, [FromBody] UpdateMajorTaskInfoDTO updateMajorTaskInfoDTO)
        {
            try
            {
                var responseMajorTask = _mapper.Map<MajorTaskResponseDTO>(await _majorTaskService.UpdateMajorTaskInfo(id, updateMajorTaskInfoDTO));
                return Ok(responseMajorTask);
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
    }
}
