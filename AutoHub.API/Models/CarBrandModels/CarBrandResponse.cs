using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;

namespace AutoHub.API.Models.CarBrandModels;

public record CarBrandResponse
{
    public IEnumerable<CarBrandResponseDTO> CarBrands { get; init; }

    public PagingInfo Paging { get; init; }
}
