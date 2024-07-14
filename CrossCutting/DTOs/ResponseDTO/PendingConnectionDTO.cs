namespace CrossCutting.DTOs.ResponseDTO;

public class PendingConnectionDTO : IdentityResponseDTO
{
    public string SenderName { get; set; }
    public string ProfilePicture { get; set; }
    public DateTimeOffset Time { get; set; }
}