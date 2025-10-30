using System.Threading.Tasks;
using AutoHub.Domain.Entities.Identity;

namespace AutoHub.BusinessLogic.Interfaces;

public interface IAuthenticationService
{
    Task<string> GenerateWebTokenForUser(ApplicationUser user);

    Task<string> HashPassword(string password);

    Task<bool> VerifyPassword(string password, string hashedPassword);
}
