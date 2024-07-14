namespace CrossCutting.DTOs.ResponseDTO
{
    public class ConnectionDTO : IdentityResponseDTO
    {
        public string UserId { get; set; }
        public string ConnectedUserName { get; set; }
        public string ProfilePicture { get; set; }
        public string Email { get; set; }
    }
}
