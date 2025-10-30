using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoHub.BusinessLogic.Configuration;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Crypto = BCrypt.Net.BCrypt;

namespace AutoHub.BusinessLogic.Services;

public class AuthenticationService(IOptions<JwtConfiguration> jwtOptions, RoleManager<ApplicationRole> roleManager)
    : IAuthenticationService
{
    private readonly JwtConfiguration jwtOptions = jwtOptions.Value;
    private readonly IList<ApplicationRole> roles = roleManager.Roles.ToList();

    public Task<string> GenerateWebTokenForUser(ApplicationUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.UserName ?? throw new ArgumentNullException(user.UserName)),
            new(JwtRegisteredClaimNames.Email, user.Email ?? throw new ArgumentNullException(user.Email))
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name ?? throw new ArgumentNullException(user.Email))));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(jwtOptions.HoursToExpire),
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature),
            Audience = jwtOptions.Audience,
            Issuer = jwtOptions.Issuer
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Task.FromResult(tokenHandler.WriteToken(token));
    }

    public Task<string> HashPassword(string password) => Task.FromResult(Crypto.HashPassword(password));

    public Task<bool> VerifyPassword(string password, string hashedPassword) => Task.FromResult(Crypto.Verify(password, hashedPassword));
}