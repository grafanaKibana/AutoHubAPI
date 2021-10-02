using System;
using System.Collections.Generic;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL.Entities
{
    public class User
    {
        public User()
        {
            UserLots = new HashSet<Lot>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationTime { get; set; }

        public UserRoleId UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public IEnumerable<Lot> UserLots { get; set; }
    }
}