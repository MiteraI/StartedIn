using Domain.Entities;

namespace Service.Services.Interface
{
    public interface ITokenService
    {
        public string CreateTokenForAccount(User user);

        public string GenerateRefreshToken();
    }
}