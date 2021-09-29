using System;
using AutoHub.DAL.Entities;

namespace AutoHub.BLL.Models
{
    public class LotModel
    {
        public int LotId { get; set; }
        public User User { get; set; }
        public Car Car { get; set; }
        public User Winner { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal StartPrice { get; set; }
        public decimal LastBid { get; set; }
    }
}