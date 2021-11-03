using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
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
            var tokenKey = Encoding.ASCII.GetBytes(_jwtOptions.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new GenericIdentity(user.Email, "Token"),
                    new[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.UserRole.UserRoleName),
                        new Claim("UserId", user.UserId.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpirationDate),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

            /*var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.UserRoleName),
                new Claim("UserId", user.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Issuer,
                claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(_jwtOptions.ExpirationDate));

            return new JwtSecurityTokenHandler().WriteToken(token);*/
        }

        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return Crypto.Verify(password, hashedPassword);
        }
    }
}