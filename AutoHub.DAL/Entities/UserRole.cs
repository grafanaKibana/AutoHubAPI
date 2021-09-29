using System.Collections.Generic;
using AutoHub.DAL.Enums;

namespace AutoHub.DAL.Entities
{
    public class UserRole
    {
        public UserRole()
        {
            Users = new List<User>();
        }
        
        public UserRoleId UserRoleId { get; set; }
        public string UserRoleName { get; set; }

        private IEnumerable<User> Users { get; set; } 
    }
}