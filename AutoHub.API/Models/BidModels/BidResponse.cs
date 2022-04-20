using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.BidDTOs;

namespace AutoHub.API.Models.BidModels;

public record BidResponse
{
    public IEnumerable<BidResponseDTO> Bids { get; init; }

    public PagingInfo Paging { get; init; }
}
