using System;
using AutoHub.DAL.Enums;
using AutoHub.DAL.Interfaces;

namespace AutoHub.DAL.Entities
{
    public class Lot
    {
        public int LotId { get; set; }
        public User Creator { get; set; }
        public Car Car { get; set; }
        public User Winner { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal StartPrice { get; set; }
        public decimal LastBid { get; set; }

        public LotStatusId LotStatusId { get; set; }
        public LotStatus LotStatus { get; set; }
    }
}