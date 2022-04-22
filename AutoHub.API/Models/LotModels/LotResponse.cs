using AutoHub.BusinessLogic.DTOs.LotDTOs;
using System.Collections.Generic;

namespace AutoHub.API.Models.LotModels;

public class LotResponse
{
    public IEnumerable<LotResponseDTO> Lots { get; init; }

    public PagingInfo Paging { get; init; }
}