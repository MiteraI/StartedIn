namespace CrossCutting.DTOs.ResponseDTO;

public class MinorTaskResponseDTO
{
    public int Position { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string MajorTaskId { get; set; }
    public string TaskboardId { get; set; }
}