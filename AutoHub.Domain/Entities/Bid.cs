using AutoHub.Domain.Entities.Identity;

namespace AutoHub.Domain.Entities;

public class Bid
{
    public int BidId { get; set; }

    public int UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public int LotId { get; set; }
    public virtual Lot Lot { get; set; }

    public decimal BidValue { get; set; }

    public DateTime BidTime { get; set; }
}