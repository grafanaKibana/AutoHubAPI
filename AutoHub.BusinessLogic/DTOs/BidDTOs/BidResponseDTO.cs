using System;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.BusinessLogic.DTOs.UserDTOs;

namespace AutoHub.BusinessLogic.DTOs.BidDTOs;

public record BidResponseDTO
{
    public int BidId { get; set; }

    public UserResponseDTO User { get; set; }

    public LotResponseDTO Lot { get; set; }

    public decimal BidValue { get; set; }

    public DateTime BidTime { get; set; }
}
