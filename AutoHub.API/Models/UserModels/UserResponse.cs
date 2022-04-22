using AutoHub.BusinessLogic.DTOs.UserDTOs;
using System.Collections.Generic;

namespace AutoHub.API.Models.UserModels;

public record UserResponse
{
    public IEnumerable<UserResponseDTO> Users { get; init; }

    public PagingInfo Paging { get; init; }
}