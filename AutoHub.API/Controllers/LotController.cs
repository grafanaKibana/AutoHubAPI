using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.LotModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult GetActiveLots()
        {
            try
            {
                var lots = _lotService.GetActiveLots();
                var mappedLots = _mapper.Map<IEnumerable<LotResponseModel>>(lots);
                return Ok(mappedLots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public IActionResult CreateLot([FromBody] LotCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedLot = _mapper.Map<Lot>(model);
                _lotService.CreateLot(mappedLot);

                return StatusCode(201, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLot(int id, [FromBody] LotUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                if (_lotService.GetById(id) == null)
                    return NotFound();

                if (!Enum.IsDefined(typeof(LotStatusEnum), model.LotStatusId))
                    return NotFound("Incorrect Status ID");

                var mappedLot = _mapper.Map<Lot>(model);
                _lotService.UpdateLot(mappedLot);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}/SetStatus")]
        public IActionResult UpdateLotStatus(int id, int statusId)
        {
            try
            {
                //TODO: Where to check for NotFound?
                if (_lotService.GetById(id) == null)
                    return NotFound("Lot not found");

                if (!Enum.IsDefined(typeof(LotStatusEnum), statusId))
                    return NotFound("Incorrect Status ID");


                var success = _lotService.SetStatus(id, statusId);
                return success ? Ok(success) : StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /*
        [HttpPut("{id}/SetWinner")]
        public IActionResult SetWinner(int id, int winnerId)
        {
            try
            {
                if (_lotService.GetById(id) == null)
                    return NotFound("Lot not found");
                
                var success = _lotService.SetWinner(id, winnerId);
                return success ? Ok(success) : StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }*/
    }
}