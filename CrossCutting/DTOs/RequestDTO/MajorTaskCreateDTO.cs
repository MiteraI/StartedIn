namespace CrossCutting.DTOs.RequestDTO;

public class MajorTaskCreateDTO
{
    public int Position { get; set; }
    public string PhaseId { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
}