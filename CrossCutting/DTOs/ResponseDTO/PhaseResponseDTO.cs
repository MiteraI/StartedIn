namespace CrossCutting.DTOs.ResponseDTO;

public class PhaseResponseDTO : IdentityResponseDTO
{
    public int Position { get; set; }
    public string PhaseName { get; set; }
    public IEnumerable<MajorTaskResponseDTO> MajorTasks { get; set; }
}