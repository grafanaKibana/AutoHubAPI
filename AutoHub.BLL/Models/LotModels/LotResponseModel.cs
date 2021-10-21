using System;
using AutoHub.BLL.Models.CarModels;
using AutoHub.BLL.Models.UserModels;

namespace AutoHub.BLL.Models.LotModels
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