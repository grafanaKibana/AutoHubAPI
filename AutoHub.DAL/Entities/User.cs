using System;
using System.Collections.Generic;
using AutoHub.DAL.Enums;

namespace AutoHub.DAL.Entities
{
    public class User
    {
        public User()
        {
            UserBids = new List<Bid>();
            UserLots = new List<Lot>();
            VictoryLots = new List<Lot>();
        }

        public int UserId { get; set; }

        public UserRoleId UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public DateTime RegistrationTime { get; set; }

        public virtual IEnumerable<Bid> UserBids { get; set; }

        public virtual IEnumerable<Lot> UserLots { get; set; }

        public virtual IEnumerable<Lot> VictoryLots { get; set; }
    }
}