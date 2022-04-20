using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.CarColorDTOs;

namespace AutoHub.API.Models.CarColorModels;

public record CarColorResponse
{
    public IEnumerable<CarColorResponseDTO> CarColors { get; init; }

    public PagingInfo Paging { get; init; }
}
