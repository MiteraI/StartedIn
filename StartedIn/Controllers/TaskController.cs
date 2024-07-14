using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;

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
}