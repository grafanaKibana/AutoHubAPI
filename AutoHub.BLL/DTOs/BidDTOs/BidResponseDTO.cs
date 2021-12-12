using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.BLL.DTOs.UserDTOs;
using System;

namespace AutoHub.BLL.DTOs.BidDTOs
{
    public class BidResponseDTO
    {
        public int BidId { get; set; }

        public UserResponseDTO User { get; set; }

        public LotResponseDTO Lot { get; set; }

        public decimal BidValue { get; set; }

        public DateTime BidTime { get; set; }
    }
}