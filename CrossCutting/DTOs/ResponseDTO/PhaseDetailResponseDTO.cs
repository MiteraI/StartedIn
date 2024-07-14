namespace CrossCutting.DTOs.ResponseDTO;

public class PhaseDetailResponseDTO : IdentityResponseDTO
{
    public string ProjectId { get; set; }
    public string PhaseName { get; set; }
    public TaskboardResponseDTO taskboardResponseDto { get; set; }
}