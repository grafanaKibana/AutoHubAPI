using AutoHub.API.Models.LotModels;
using AutoHub.API.Models.UserModels;
using System;

namespace AutoHub.API.Models.BidModels;

public record BidResponse
{
    public int BidId { get; init; }

    public UserResponse User { get; init; }

    public LotResponse Lot { get; init; }

    public decimal BidValue { get; init; }

    public DateTime BidTime { get; init; }
}
