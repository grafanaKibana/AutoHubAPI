using System;
using AutoHub.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotController : Controller
    {
        private readonly ILotService _lotService;

        public LotController(ILotService lotService)
        {
            _lotService = lotService;
        }

        [HttpGet]
        public IActionResult GetAllLots()
        {
            try
            {
                return Ok(_lotService.GetAll());
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
                return Ok(_lotService.GetActiveLots());
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
                return Ok(lot);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}