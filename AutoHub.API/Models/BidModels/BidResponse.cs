using AutoHub.API.Models.LotModels;
using AutoHub.API.Models.UserModels;
using System;
using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.BidDTOs;

namespace AutoHub.API.Models.BidModels;

public record BidResponse
{
    public IEnumerable<BidResponseDTO> Bids { get; set; }

    public PagingInfo Paging { get; set; }
}
