using CrossCutting.DTOs.RequestDTO;
using Domain.Entities;

namespace Service.Services.Interface;

public interface IPhaseService
{
    Task<string> CreateNewPhase(PhaseCreateDTO phaseCreateDto);

    Task<Phase> GetPhaseDetailById(string id);

    Task<Phase> MovePhase(string phaseId, int position, bool needsReposition);
}