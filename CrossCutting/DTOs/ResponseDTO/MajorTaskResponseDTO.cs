namespace CrossCutting.DTOs.ResponseDTO;

public class MajorTaskResponseDTO : IdentityResponseDTO
{
    public int Position { get; set; }
    public string TaskTitle { get; set; }
    public string Description { get; set; }
}