using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
using System.Collections.Generic;

namespace AutoHub.API.Models.CarBrandModels;

public record CarBrandResponse
{
    public IEnumerable<CarBrandResponseDTO> CarBrands { get; init; }

    public PagingInfo Paging { get; init; }
}