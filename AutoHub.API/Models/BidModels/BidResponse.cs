using AutoHub.BusinessLogic.DTOs.BidDTOs;
using System.Collections.Generic;

namespace AutoHub.API.Models.BidModels;

public record BidResponse
{
    public IEnumerable<BidResponseDTO> Bids { get; init; }

    public PagingInfo Paging { get; init; }
}