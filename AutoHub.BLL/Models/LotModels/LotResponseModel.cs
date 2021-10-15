using System;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Models.LotModels
{
    public class LotResponseModel
    {
        public int LotId { get; set; }
        public User Creator { get; set; }
        public Car Car { get; set; }
        public User Winner { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}