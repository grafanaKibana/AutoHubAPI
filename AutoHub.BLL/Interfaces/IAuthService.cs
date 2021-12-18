using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Interfaces
{
    public interface IAuthService
    {
        string GenerateWebTokenForUser(User user);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}