namespace CrossCutting.DTOs.ResponseDTO;

public class TaskboardResponseDTO : IdentityResponseDTO
{
    public string Title { get; set; }
    public string PhaseId { get; set; }
    public int Position { get; set; }
    public MinorTaskInTaskboardResponseDTO MinorTaskInTaskboardResponseDto { get; set; }
}