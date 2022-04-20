using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.CarModelDTOs;

namespace AutoHub.API.Models.CarModelModels;

public record CarModelResponse
{
    public IEnumerable<CarModelResponseDTO> CarModels { get; init; }

    public PagingInfo Paging { get; init; }
}
