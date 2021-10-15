using System;

namespace AutoHub.BLL.Models.LotModels
{
    public class LotCreateRequestModel
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public int? WinnerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal StartPrice { get; set; }
        public decimal LastBid { get; set; }
    }
}