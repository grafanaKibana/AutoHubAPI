using System;
using System.Collections.Generic;
using System.Net;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/Lots/{lotId}/Bids")]
    [ApiController]
    public class LotBidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly ILotService _lotService;
        private readonly IMapper _mapper;

        public LotBidController(IBidService bidService, ILotService lotService, IMapper mapper)
        {
            _bidService = bidService;
            _lotService = lotService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetLotBids(int lotId)
        {
            try
            {
                var lot = _lotService.GetById(lotId);

                if (lot == null)
                    return NotFound();

                var bids = _bidService.GetLotBids(lotId);

                var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);
                return Ok(mappedBids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateBid(int lotId, [FromBody] BidCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var lot = _lotService.GetById(lotId);

                if (lot == null)
                    return NotFound();

                var mappedBid = _mapper.Map<BidCreateRequestDTO>(model);
                _bidService.Create(lotId, mappedBid);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}