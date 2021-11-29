using System.Collections.Generic;
using System.Net;
using AutoHub.API.Models.LotModels;
using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.BLL.Interfaces;
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
            var lots = _lotService.GetAll();
            var mappedLots = _mapper.Map<IEnumerable<LotResponseModel>>(lots);
            return Ok(mappedLots);
        }

        [HttpGet("Active")]
        [ProducesResponseType(typeof(IEnumerable<LotResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetActiveLots()
        {
            var lots = _lotService.GetActive();
            var mappedLots = _mapper.Map<IEnumerable<LotResponseModel>>(lots);
            return Ok(mappedLots);
        }

        [HttpGet("{lotId}")]
        [ProducesResponseType(typeof(LotResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLotById(int lotId)
        {
            var lot = _lotService.GetById(lotId);

            var mappedLot = _mapper.Map<LotResponseModel>(lot);
            return Ok(mappedLot);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateLot([FromBody] LotCreateRequestModel model)
        {
            if (model == null) return BadRequest();

            var mappedLot = _mapper.Map<LotCreateRequestDTO>(model);
            _lotService.Create(mappedLot);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{lotId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateLot(int lotId, [FromBody] LotUpdateRequestModel model)
        {
            if (model == null) return BadRequest();

            var mappedLot = _mapper.Map<LotUpdateRequestDTO>(model);

            _lotService.Update(lotId, mappedLot);
            return NoContent();
        }

        [HttpDelete("{lotId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLot(int lotId)
        {
            _lotService.Delete(lotId);
            return NoContent();
        }
    }
}