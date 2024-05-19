using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface ITokenService
    {
        public string CreateTokenForAccount(User user);

        public string GenerateRefreshToken();
    }
}