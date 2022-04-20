using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.CarDTOs;

namespace AutoHub.API.Models.CarModels;

public record CarResponse
{
    public IEnumerable<CarResponseDTO> Cars { get; init; }

    public PagingInfo Paging { get; init; }
}
