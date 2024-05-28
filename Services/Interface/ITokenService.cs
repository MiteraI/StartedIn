using Domain.Entities;

namespace Repositories.Interface
{
    public interface ITokenService
    {
        public string CreateTokenForAccount(User user);

        public string GenerateRefreshToken();
    }
}