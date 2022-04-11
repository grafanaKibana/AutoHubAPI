using AutoHub.API.Models.LotModels;
using AutoHub.API.Models.UserModels;
using System;

namespace AutoHub.API.Models.BidModels;

public class BidResponse
{
    public int BidId { get; set; }

    public UserResponse User { get; set; }

    public LotResponse Lot { get; set; }

    public decimal BidValue { get; set; }

    public DateTime BidTime { get; set; }
}
