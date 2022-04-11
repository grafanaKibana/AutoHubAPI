using AutoHub.Domain.Entities.Identity;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IAuthenticationService
{
    string GenerateWebTokenForUser(ApplicationUser user);

    string HashPassword(string password);

    bool VerifyPassword(string password, string hashedPassword);
}
