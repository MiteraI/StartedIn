using CrossCutting.Enum;

namespace CrossCutting.DTOs.ResponseDTO;

public class MinorTaskInTaskboardResponseDTO : IdentityResponseDTO
{
    public int Position { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    public MinorTaskStatus Status { get; set; }
    public string? MajorTaskId { get; set; }
}