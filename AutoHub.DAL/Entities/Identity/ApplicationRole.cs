using Microsoft.AspNetCore.Identity;

namespace AutoHub.DAL.Entities.Identity
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
        { }

        public ApplicationRole(string name)
        {
            Name = name;
        }
    }
}