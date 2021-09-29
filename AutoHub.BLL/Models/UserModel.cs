using System;
using System.Collections.Generic;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationTime { get; set; }

        public IEnumerable<Lot> UserLots { get; set; }
    }
}