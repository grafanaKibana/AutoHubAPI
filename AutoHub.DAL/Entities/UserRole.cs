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

        public UserRoleEnum UserRoleId { get; set; }
        public string UserRoleName { get; set; }

        public virtual IEnumerable<User> Users { get; }
    }
}