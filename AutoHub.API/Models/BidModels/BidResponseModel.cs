using System;
using AutoHub.API.Models.LotModels;
using AutoHub.API.Models.UserModels;

namespace AutoHub.API.Models.BidModels
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