using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.LotModels;
using AutoHub.DAL.Entities;
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
        public IActionResult GetLotById(int id)
        {
            try
            {
                var lot = _lotService.GetById(id);
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

                return Ok(mappedLot);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}