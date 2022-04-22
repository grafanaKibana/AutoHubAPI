using AutoHub.BusinessLogic.DTOs.CarDTOs;
using AutoHub.BusinessLogic.DTOs.UserDTOs;
using System;

namespace AutoHub.BusinessLogic.DTOs.LotDTOs;

public record LotResponseDTO
{
    public int LotId { get; set; }
    public string LotStatus { get; set; }
    public UserResponseDTO Creator { get; set; }
    public CarResponseDTO Car { get; set; }
    public UserResponseDTO Winner { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}