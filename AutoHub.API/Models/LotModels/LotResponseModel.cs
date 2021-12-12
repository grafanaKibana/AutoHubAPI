using AutoHub.API.Models.CarModels;
using AutoHub.API.Models.UserModels;
using System;

namespace AutoHub.API.Models.LotModels
{
    public class LotResponseModel
    {
        public int LotId { get; set; }
        public string LotStatus { get; set; }
        public UserResponseModel Creator { get; set; }
        public CarResponseModel Car { get; set; }
        public UserResponseModel Winner { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}