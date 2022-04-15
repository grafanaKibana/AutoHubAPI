using AutoHub.API.Models.CarModels;
using AutoHub.API.Models.UserModels;
using System;

namespace AutoHub.API.Models.LotModels;

public class LotResponse
{
    public int LotId { get; init; }
    public string LotStatus { get; init; }
    public UserResponse Creator { get; init; }
    public CarResponse Car { get; init; }
    public UserResponse Winner { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime? EndTime { get; init; }
}
