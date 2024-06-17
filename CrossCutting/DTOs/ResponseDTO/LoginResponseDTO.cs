namespace CrossCutting.DTOs.ResponseDTO
{
    public class LoginResponseDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
