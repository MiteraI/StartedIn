using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Services.Interface;
using Services.Exceptions;

namespace StartedIn.Controllers;

[ApiController]
[Route("api")]
public class TaskController : ControllerBase
{
    private readonly IMajorTaskService _majorTaskService;
    private readonly IMapper _mapper;
    private readonly ILogger<TaskController> _logger;
    private readonly IMinorTaskService _minorTaskService;
    private readonly ITaskboardService _taskboardService;

    public TaskController(IMajorTaskService majorTaskService, IMapper mapper, ILogger<TaskController> logger, IMinorTaskService minorTaskService,
        ITaskboardService taskboardService)
    {
        _majorTaskService = majorTaskService;
        _minorTaskService = minorTaskService;
        _taskboardService = taskboardService;
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
            return StatusCode(500,"Lỗi server");
        }
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
            return StatusCode(500,"Lỗi server");
        }
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
            return StatusCode(500,"Lỗi server");
        }
    }

    [HttpPut("majortask/move")]
    public async Task<ActionResult<MajorTaskResponseDTO>> MoveMajorTask(UpdateMajorTaskPositionDTO updateMajorTaskPositionDTO)
    {
        try
        {
            var responseMajorTask = _mapper.Map<MajorTaskResponseDTO>(await _majorTaskService.MoveMajorTask(updateMajorTaskPositionDTO.Id, updateMajorTaskPositionDTO.PhaseId,updateMajorTaskPositionDTO.Position, updateMajorTaskPositionDTO.NeedsReposition));
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

}