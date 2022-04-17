using AutoHub.API.Models.CarModels;
using AutoHub.API.Models.UserModels;
using System;
using System.Collections.Generic;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using Newtonsoft.Json;

namespace AutoHub.API.Models.LotModels;

public class LotResponse
{
    public IEnumerable<LotResponseDTO> Lots { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public PagingInfo Paging { get; set; }
    
}
