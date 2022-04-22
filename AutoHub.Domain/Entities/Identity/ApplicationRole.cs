using Microsoft.AspNetCore.Identity;

namespace AutoHub.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public ApplicationRole()
    { }

    public ApplicationRole(string name)
    {
        Name = name;
    }
}