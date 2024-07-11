using CrossCutting.DTOs.RequestDTO;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Interface;

namespace StartedIn.Controllers;

[ApiController]
[Route("api")]
public class PhaseController : ControllerBase
{
    private readonly IPhaseService _phaseService;
    private readonly ILogger<PhaseController> _logger;

    public PhaseController(IPhaseService phaseService, ILogger<PhaseController> logger)
    {
        _phaseService = phaseService;
        _logger = logger;
    }
    
    [HttpPost("phase/create")]
    public async Task<IActionResult> CreateNewPhase(PhaseCreateDTO phaseCreateDto)
    {
        try
        {
            await _phaseService.CreateNewPhase(phaseCreateDto);
            return StatusCode(201, "Tạo phase thành công");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating new phase.");
            return StatusCode(500,"Lỗi server");
        }
    }
}