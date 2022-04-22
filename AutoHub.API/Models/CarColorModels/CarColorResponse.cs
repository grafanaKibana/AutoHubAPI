using AutoHub.BusinessLogic.DTOs.CarColorDTOs;
using System.Collections.Generic;

namespace AutoHub.API.Models.CarColorModels;

public record CarColorResponse
{
    public IEnumerable<CarColorResponseDTO> CarColors { get; init; }

    public PagingInfo Paging { get; init; }
}