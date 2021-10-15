using System;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.BidModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : Controller
    {
        private readonly IBidService _bidService;

        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet("GetByUser/{userId}")]
        public IActionResult GetAllBidsByUser(int userId)
        {
            try
            {
                var bids = _bidService.GetAllUserBids(userId);
                if (bids == null)
                    return NotFound();
                return Ok(bids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetByLot/{lotId}")]
        public IActionResult GetAllBidsByLot(int lotId)
        {
            try
            {
                var bids = _bidService.GetAllLotBids(lotId);
                if (bids == null)
                    return NotFound();
                return Ok(bids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBidById(int id)
        {
            try
            {
                var bid = _bidService.GetById(id);
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
        public IActionResult CreateBid([FromBody] BidCreateRequestModel bidCreateRequestModel)
        {
            try
            {
                if (bidCreateRequestModel == null)
                    return BadRequest();

                _bidService.CreateBid(bidCreateRequestModel);
                return Ok(bidCreateRequestModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}