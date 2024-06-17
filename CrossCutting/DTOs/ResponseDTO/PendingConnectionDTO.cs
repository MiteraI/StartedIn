namespace CrossCutting.DTOs.ResponseDTO;

public class PendingConnectionDTO
{
    public string Id { get; set; }
    public string SenderName { get; set; }
    public string ProfilePicture { get; set; }
    public DateTimeOffset Time { get; set; }
}