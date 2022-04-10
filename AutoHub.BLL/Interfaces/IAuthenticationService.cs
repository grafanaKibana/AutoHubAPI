using AutoHub.DAL.Entities.Identity;

namespace AutoHub.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateWebTokenForUser(ApplicationUser user);

        string HashPassword(string password);

        bool VerifyPassword(string password, string hashedPassword);
    }
}