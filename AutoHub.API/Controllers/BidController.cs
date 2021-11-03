using System;
using System.Net;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;

        public BidController(IBidService bidService, IMapper mapper)
        {
            _bidService = bidService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateBid([FromBody] BidCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedBid = _mapper.Map<BidCreateRequestDTO>(model);
                _bidService.Create(mappedBid);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}