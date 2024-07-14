using AutoMapper;
using CrossCutting.DTOs.RequestDTO;
using CrossCutting.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;
using Services.Exceptions;

namespace StartedIn.Controllers;

[ApiController]
[Route("api")]
public class PhaseController : ControllerBase
{
    private readonly IPhaseService _phaseService;
    private readonly ILogger<PhaseController> _logger;
    private readonly IMapper _mapper;

    public PhaseController(IPhaseService phaseService, ILogger<PhaseController> logger, IMapper mapper)
    {
        _phaseService = phaseService;
        _logger = logger;
        _mapper = mapper;
    }
    
    [HttpPost("phase/create")]
    public async Task<IActionResult> CreateNewPhase(PhaseCreateDTO phaseCreateDto)
    {
        try
        {
            string id = await _phaseService.CreateNewPhase(phaseCreateDto);
            return StatusCode(201, new { message = "Tạo phase thành công", id = id });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating new phase.");
            return StatusCode(500,"Lỗi server");
        }
    }

    [HttpGet("phase/{id}")]
    public async Task<ActionResult<PhaseDetailResponseDTO>> GetPhaseDetailById(string id)
    {
        try
        {
            var phase = await _phaseService.GetPhaseDetailById(id);
            var mappedPhase = _mapper.Map<PhaseDetailResponseDTO>(phase);
            return Ok(mappedPhase);
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