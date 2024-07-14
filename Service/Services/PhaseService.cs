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

    public async Task<Phase> MovePhase(string phaseId, int position, bool needsReposition)
    {
        var chosenPhase = await _phaseRepository.GetOneAsync(phaseId);
        if (chosenPhase == null)
        {
            throw new NotFoundException("Không có giai đoạn được tìm thấy");
        }
        try
        {
            chosenPhase.Position = position;
            _phaseRepository.Update(chosenPhase);
            await _unitOfWork.SaveChangesAsync();
            if (needsReposition)
            {
                _unitOfWork.BeginTransaction();
                // Get all phases for the project and sort them by position
                var phases = await _phaseRepository.QueryHelper()
                    .Filter(p => p.ProjectId.Equals(chosenPhase.ProjectId))
                    .OrderBy(p => p.OrderBy(p => p.Position))
                    .GetAllAsync();

                // Update positions starting from 2^16 (65536)
                int increment = (int)Math.Pow(2, 16);
                int currentPosition = (int)Math.Pow(2, 16);

                foreach (var phase in phases)
                {
                    phase.Position = currentPosition;
                    _phaseRepository.Update(phase);
                    currentPosition += increment;
                    await _unitOfWork.SaveChangesAsync();   
                }
                await _unitOfWork.CommitAsync();
            }
            return chosenPhase;
        }

        catch (Exception ex) {
            _logger.LogError(ex, "Error while moving phase");
            await _unitOfWork.RollbackAsync();
            throw;
        }
       
    }
}