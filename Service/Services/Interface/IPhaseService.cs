using CrossCutting.DTOs.RequestDTO;

namespace Service.Services.Interface;

public interface IPhaseService
{
    Task<string> CreateNewPhase(PhaseCreateDTO phaseCreateDto);
}