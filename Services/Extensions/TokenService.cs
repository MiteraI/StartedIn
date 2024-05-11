using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string CreateTokenForAccount(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, account.Role),
                new(ClaimTypes.Email,account.Email),
                new("userId", account.Id)
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["jwt:secret"]));

            var credential = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                _configuration["jwt:issuer"],
                _configuration["jwt:audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credential);

            return tokenHandler.WriteToken(token);
        }
    }
}
