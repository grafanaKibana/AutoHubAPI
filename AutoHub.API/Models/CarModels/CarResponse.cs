using AutoHub.BusinessLogic.DTOs.CarDTOs;
using System.Collections.Generic;

namespace AutoHub.API.Models.CarModels;

public record CarResponse
{
    public IEnumerable<CarResponseDTO> Cars { get; init; }

    public PagingInfo Paging { get; init; }
}