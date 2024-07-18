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
    public class TaskBoardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<TaskBoardController> _logger;
        private readonly ITaskboardService _taskboardService;

        public TaskBoardController(IMapper mapper, ILogger<TaskBoardController> logger, ITaskboardService taskboardService)
        {
            _taskboardService = taskboardService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("taskboard/create")]
        public async Task<ActionResult<TaskboardResponseDTO>> CreateNewTaskboard(TaskboardCreateDTO taskboardCreateDto)
        {
            try
            {
                var taskboard = await _taskboardService.CreateNewTaskboard(taskboardCreateDto);
                var response = _mapper.Map<TaskboardResponseDTO>(taskboard);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating new taskboard.");
                return StatusCode(500, "Lỗi server");
            }
        }

        [HttpPut("taskboard/move")]
        public async Task<ActionResult<TaskboardResponseDTO>> MoveTaskBoard(UpdateTaskBoardPositionDTO updatetaskBoardPositionDTO)
        {
            try
            {
                var responseTaskBoard = _mapper.Map<TaskboardResponseDTO>(await _taskboardService.MoveTaskBoard(updatetaskBoardPositionDTO.Id, updatetaskBoardPositionDTO.Position, updatetaskBoardPositionDTO.NeedsReposition));
                return Ok(responseTaskBoard);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Di chuyển bảng làm việc thất bại");
            }
        }

        [HttpGet("taskboard/{id}")]
        public async Task<ActionResult<TaskboardResponseDTO>> GetTaskboardById([FromRoute] string id)
        {
            try
            {
                var response = _mapper.Map<TaskboardResponseDTO>(await _taskboardService.GetTaskboardById(id));
                return Ok(response);
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
    }
}
