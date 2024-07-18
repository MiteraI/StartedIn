using CrossCutting.Enum;

namespace CrossCutting.DTOs.RequestDTO;

public class UpdateMinorTaskInfoDTO
{
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    public MinorTaskStatus Status { get; set; }
    public string? MajorTaskId { get; set; }
}