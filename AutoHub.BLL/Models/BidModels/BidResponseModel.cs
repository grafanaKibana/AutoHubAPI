using System;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Models.BidModels
{
    public class BidResponseModel
    {
        public int BidId { get; set; }

        public User User { get; set; }

        public Lot Lot { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }
    }
}