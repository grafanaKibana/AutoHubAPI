using AutoHub.DAL.Enums;
using System;
using System.Collections.Generic;
using AutoHub.DAL.Entities.Identity;

namespace AutoHub.DAL.Entities
{
    public class Lot
    {
        public Lot()
        {
            Bids = new List<Bid>();
        }

        public int LotId { get; set; }

        public LotStatusEnum LotStatusId { get; set; }
        public virtual LotStatus LotStatus { get; set; }

        public int CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }

        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        public int? WinnerId { get; set; }
        public virtual ApplicationUser Winner { get; set; }

        public virtual IEnumerable<Bid> Bids { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
    }
}