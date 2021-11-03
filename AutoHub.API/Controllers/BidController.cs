using AutoHub.BLL.Interfaces;
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
    }
}