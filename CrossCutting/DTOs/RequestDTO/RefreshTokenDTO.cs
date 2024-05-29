namespace CrossCutting.DTOs.RequestDTO;

public class RefreshTokenDTO
{
    public required string JwtToken { get; set; }
    public required string RefreshToken { get; set; }
}