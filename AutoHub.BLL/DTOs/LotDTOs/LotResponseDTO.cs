using System;
using AutoHub.BLL.DTOs.CarDTOs;
using AutoHub.BLL.DTOs.UserDTOs;

namespace AutoHub.BLL.DTOs.LotDTOs
{
    public class LotResponseDTO
    {
        public int LotId { get; set; }
        public string LotStatus { get; set; }
        public UserResponseDTO Creator { get; set; }
        public CarResponseDTO Car { get; set; }
        public UserResponseDTO Winner { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}