using System;
using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.UserDTOs;

namespace AutoHub.API.Models.UserModels;

public record UserResponse
{
    public IEnumerable<UserResponseDTO> Users { get; init; }

    public PagingInfo Paging { get; init; }
}
