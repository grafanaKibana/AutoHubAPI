using System;
using System.Collections.Generic;
using AutoHub.API.Models.BidModels;
using AutoHub.API.Models.LotModels;
using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class LotController : Controller
    {
        private readonly ILotService _lotService;
        private readonly IMapper _mapper;


        public LotController(ILotService lotService, IMapper mapper)
        {
            _lotService = lotService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LotResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAllLots()
        {
            try
            {
                var lots = _lotService.GetAll();
                var mappedLots = _mapper.Map<IEnumerable<LotResponseModel>>(lots);
                return Ok(mappedLots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("Active")]
        [ProducesResponseType(typeof(IEnumerable<LotResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetActiveLots()
        {
            try
            {
                var lots = _lotService.GetActive();
                var mappedLots = _mapper.Map<IEnumerable<LotResponseModel>>(lots);
                return Ok(mappedLots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{lotId}")]
        [ProducesResponseType(typeof(LotResponseModel), StatusCodes.Status200OK)]
        public IActionResult GetLotById(int lotId)
        {
            try
            {
                var lot = _lotService.GetById(lotId);
                if (lot == null)
                    return NotFound();

                var mappedLot = _mapper.Map<LotResponseModel>(lot);
                return Ok(mappedLot);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{lotId}/Bids")]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetLotBids(int id)
        {
            try
            {
                if (_lotService.GetById(id) == null)
                    return NotFound();

                var bids = _lotService.GetBids(id);
                var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);
                return Ok(mappedBids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateLot([FromBody] LotCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedLot = _mapper.Map<LotCreateRequestDTO>(model);
                _lotService.CreateLot(mappedLot);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{lotId}")]
        public IActionResult UpdateLot(int lotId, [FromBody] LotUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                if (_lotService.GetById(lotId) == null)
                    return NotFound();

                if (!Enum.IsDefined(typeof(LotStatusEnum), model.LotStatusId))
                    return NotFound("Incorrect Status ID");

                var mappedLot = _mapper.Map<LotUpdateRequestDTO>(model);
                mappedLot.LotId = lotId;

                _lotService.UpdateLot(mappedLot);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}