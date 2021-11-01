using System;

namespace AutoHub.DAL.Entities
{
    public class Bid
    {
        public int BidId { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int LotId { get; set; }
        public virtual Lot Lot { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }
    }
}