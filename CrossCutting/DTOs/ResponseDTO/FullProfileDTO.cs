namespace CrossCutting.DTOs.ResponseDTO;

public class FullProfileDTO : IdentityResponseDTO
{
    public string ProfilePicture { get; set; }
    public string CoverPhoto { get; set; }
    public string Bio { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
}