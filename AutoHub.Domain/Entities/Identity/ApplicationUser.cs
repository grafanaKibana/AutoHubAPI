using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoHub.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public ApplicationUser()
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