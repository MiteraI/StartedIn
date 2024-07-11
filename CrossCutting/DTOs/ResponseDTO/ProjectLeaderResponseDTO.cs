namespace CrossCutting.DTOs.ResponseDTO;

public class ProjectLeaderResponseDTO : IdentityResponseDTO
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string ProfilePicture { get; set; }
}