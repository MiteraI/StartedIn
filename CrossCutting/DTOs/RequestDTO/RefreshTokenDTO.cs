namespace CrossCutting.DTOs.RequestDTO;

public class RefreshTokenDTO
{
    public string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}