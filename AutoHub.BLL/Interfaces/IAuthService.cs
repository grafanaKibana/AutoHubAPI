using AutoHub.DAL.Entities;
using AutoHub.DAL.Entities.Identity;

namespace AutoHub.BLL.Interfaces
{
    public interface IAuthService
    {
        string GenerateWebTokenForUser(AppUser user);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}