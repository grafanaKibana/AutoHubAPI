using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoHub.Domain.Entities.Identity;

public sealed class ApplicationUser : IdentityUser<int>
{
    [PersonalData, Required]
    public string FirstName { get; set; }

    [PersonalData, Required]
    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public DateTime RegistrationTime { get; set; }

    public IEnumerable<Bid> UserBids { get; set; } = new List<Bid>();

    public IEnumerable<Lot> UserLots { get; set; } = new List<Lot>();

    public IEnumerable<Lot> VictoryLots { get; set; } = new List<Lot>();
}
