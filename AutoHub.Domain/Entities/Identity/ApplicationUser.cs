using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AutoHub.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<int>
{
    [PersonalData, Required]
    public string FirstName { get; set; }

    [PersonalData, Required]
    public string LastName { get; set; }

    public string FullName => $"{this.FirstName} {this.LastName}";

    public DateTime RegistrationTime { get; set; }

    public virtual IEnumerable<Bid> UserBids { get; set; } = new List<Bid>();

    public virtual IEnumerable<Lot> UserLots { get; set; } = new List<Lot>();

    public virtual IEnumerable<Lot> VictoryLots { get; set; } = new List<Lot>();
}
