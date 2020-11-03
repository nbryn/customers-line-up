using BC = BCrypt.Net.BCrypt;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

using Data;
using Logic.Models;
using Logic.Exceptions;

namespace Logic.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<LoginResponseDTO> RegisterUser(RegisterDTO user)
        {
            string token = GenerateJWTToken(user);
            user.Password = BC.HashPassword(user.Password);

            int userId = await _userRepository.Register(user);

            LoginResponseDTO response = new LoginResponseDTO
            {
                Id = userId,
                Email = user.Email,
                Token = token,
            };


            return response;

        }
        public async Task<LoginResponseDTO> Authenticate(LoginDTO loginRequest)
        {
            string token = GenerateJWTToken(loginRequest);

            UserDTO user = await _userRepository.FindByEmail(loginRequest.Email);

            if (user == null || !BC.Verify(loginRequest.Password, user.Password))
            {
                return null;
            }

            LoginResponseDTO response = new LoginResponseDTO{
                Id = user.Id,
                Email = user.Email,
                Token = token,
            };

            return response;
        }

        private string GenerateJWTToken(LoginDTO user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("role", "User"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}