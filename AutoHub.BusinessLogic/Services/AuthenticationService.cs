using AutoHub.BusinessLogic.Configuration;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Crypto = BCrypt.Net.BCrypt;

namespace AutoHub.BusinessLogic.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtConfiguration _jwtOptions;
    private readonly IList<ApplicationRole> _roles;

    public AuthenticationService(IOptions<JwtConfiguration> jwtOptions, RoleManager<ApplicationRole> roleManager)
    {
        _jwtOptions = jwtOptions.Value;
        _roles = roleManager.Roles.ToList();
    }

    public string GenerateWebTokenForUser(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_jwtOptions.Key);
        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

        claims.AddRange(_roles.Select(r => new Claim(ClaimTypes.Role, r.Name)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpirationDate),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256),
            Audience = _jwtOptions.Audience,
            Issuer = _jwtOptions.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string HashPassword(string password) => Crypto.HashPassword(password);

    public bool VerifyPassword(string password, string hashedPassword) => Crypto.Verify(password, hashedPassword);
}
