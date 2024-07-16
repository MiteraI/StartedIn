namespace CrossCutting.DTOs.RequestDTO;

public class MinorTaskCreateDTO
{
    public int Position { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
    public string TaskboardId { get; set; }
    
}