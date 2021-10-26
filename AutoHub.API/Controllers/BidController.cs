using System;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.BidModels;
using AutoHub.DAL.Entities;
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

        [HttpGet("{bidId}")]
        public IActionResult GetBidById(int bidId)
        {
            try
            {
                var bid = _bidService.GetById(bidId);
                if (bid == null)
                    return NotFound();
                return Ok(bid);
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

                var mappedBid = _mapper.Map<Bid>(model);
                _bidService.CreateBid(mappedBid);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}