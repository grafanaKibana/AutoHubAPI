using System;

namespace AutoHub.BLL.Models.BidModels
{
    public class BidCreateApiModel
    {
        public int BidId { get; set; }

        public int UserId { get; set; }

        public int LotId { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }
    }
}