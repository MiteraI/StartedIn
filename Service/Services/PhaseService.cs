using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository.Repositories.Interface;
using Service.Services.Interface;
using Services.Exceptions;

namespace Service.Services;

public class PhaseService : IPhaseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPhaseRepository _phaseRepository;
    private readonly ILogger<Phase> _logger;

    public PhaseService(IUnitOfWork unitOfWork, IPhaseRepository phaseRepository, ILogger<Phase> logger)
    {
        _unitOfWork = unitOfWork;
        _phaseRepository = phaseRepository;
        _logger = logger;
    }
    public async Task<string> CreateNewPhase(PhaseCreateDTO phaseCreateDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            Phase phase = new Phase
            {
                PhaseName = phaseCreateDto.PhaseName,
                ProjectId = phaseCreateDto.ProjectId,
                Position = phaseCreateDto.Position
            };
            var phaseEntity = _phaseRepository.Add(phase);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            return phaseEntity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating phase");
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    public async Task<Phase> GetPhaseDetailById(string id)
    {
        var phase = await _phaseRepository.GetPhaseDetailById(id);
        if (phase == null)
        {
            throw new NotFoundException("Không có phase nào");
        }

        return phase;
    }
}