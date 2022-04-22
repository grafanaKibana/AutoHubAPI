using AutoHub.BusinessLogic.DTOs.CarModelDTOs;
using System.Collections.Generic;

namespace AutoHub.API.Models.CarModelModels;

public record CarModelResponse
{
    public IEnumerable<CarModelResponseDTO> CarModels { get; init; }

    public PagingInfo Paging { get; init; }
}