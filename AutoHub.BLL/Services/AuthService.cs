using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoHub.BLL.Configuration;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Crypto = BCrypt.Net.BCrypt;

namespace AutoHub.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtConfiguration _jwtOptions;

        public AuthService(IOptions<JwtConfiguration> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateWebTokenForUser(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpirationDate),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            return Crypto.Verify(password, hashedPassword);
        }
    }
}