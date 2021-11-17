using System;
using System.Collections.Generic;
using System.Net;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateLot([FromBody] LotCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedLot = _mapper.Map<LotCreateRequestDTO>(model);
                _lotService.Create(mappedLot);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{lotId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLot(int lotId, [FromBody] LotUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                if (_lotService.GetById(lotId) == null)
                    return NotFound();

                if (!Enum.IsDefined(typeof(LotStatusEnum), model.LotStatusId))
                    return UnprocessableEntity("Incorrect lot status ID");

                var mappedLot = _mapper.Map<LotUpdateRequestDTO>(model);

                _lotService.Update(lotId, mappedLot);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{lotId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLot(int lotId)
        {
            try
            {
                if (_lotService.GetById(lotId) == null)
                    return NotFound();

                _lotService.Delete(lotId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}