using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AutoHub.DAL.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
            UserBids = new List<Bid>();
            UserLots = new List<Lot>();
            VictoryLots = new List<Lot>();
        }

        [PersonalData, Required]
        public string FirstName { get; set; }

        [PersonalData, Required]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public DateTime RegistrationTime { get; set; }

        public virtual IEnumerable<Bid> UserBids { get; set; }

        public virtual IEnumerable<Lot> UserLots { get; set; }

        public virtual IEnumerable<Lot> VictoryLots { get; set; }
    }
}
