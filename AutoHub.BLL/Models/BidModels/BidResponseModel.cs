using System;
using AutoHub.BLL.Models.LotModels;
using AutoHub.BLL.Models.UserModels;

namespace AutoHub.BLL.Models.BidModels
{
    public class BidResponseModel
    {
        public int BidId { get; set; }

        public UserResponseModel User { get; set; }

        public LotResponseModel Lot { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }
    }
}