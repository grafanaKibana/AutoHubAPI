using AutoHub.API.Models.CarModels;
using AutoHub.API.Models.UserModels;
using System;

namespace AutoHub.API.Models.LotModels;

public class LotResponse
{
    public int LotId { get; set; }
    public string LotStatus { get; set; }
    public UserResponse Creator { get; set; }
    public CarResponse Car { get; set; }
    public UserResponse Winner { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}
