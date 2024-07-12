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

    public TaskController(IMajorTaskService majorTaskService, IMapper mapper, ILogger<TaskController> logger)
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
            return StatusCode(500,"Lỗi server");
        }
    }
}