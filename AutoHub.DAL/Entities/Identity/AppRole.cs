using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AutoHub.DAL.Entities.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        public AppRole() { }

        public AppRole(string name)
        {
            Name = name;
        }
        
    }
}
