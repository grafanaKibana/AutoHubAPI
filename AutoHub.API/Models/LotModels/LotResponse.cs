
using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.LotDTOs;

namespace AutoHub.API.Models.LotModels;

public class LotResponse
{
    public IEnumerable<LotResponseDTO> Lots { get; init; }

    public PagingInfo Paging { get; init; }
    
}
