using System;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{bidId}")]
        [ProducesResponseType(typeof(BidResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetBidById(int bidId)
        {
            try
            {
                var bid = _bidService.GetById(bidId);

                if (bid == null)
                    return NotFound();

                var mappedBid = _mapper.Map<BidResponseModel>(bid);
                return Ok(mappedBid);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateBid([FromBody] BidCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedBid = _mapper.Map<BidCreateRequestDTO>(model);
                _bidService.CreateBid(mappedBid);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}