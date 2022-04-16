using System;
using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.UserDTOs;

namespace AutoHub.API.Models.UserModels;

public record UserResponse
{
    public IEnumerable<UserResponseDTO> Users { get; set; }

    public PagingInfo Paging { get; set; }
}
