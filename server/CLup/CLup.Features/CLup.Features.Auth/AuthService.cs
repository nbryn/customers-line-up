using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CLup.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config) => _config = config;
        
        public string GenerateJWTToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim("role", "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}