namespace AutoHub.API.Controllers
{
    /*
    [Route("api/Users/{userId}/Bids")]
    [ApiController]
    public class UserBidController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBidService _bidService;

        public UserBidController(IBidService userService, IMapper mapper)
        {
            _bidService = userService;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetUserBids(int userId)
        {
            try
            {
                var bids = _bidService.GetUserBids(userId);
                if (bids == null)
                    return NotFound();
                
                var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);
                return Ok(mappedBids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpGet("{bidId}")]
        public IActionResult GetUserBidById(int userId, int bidId)
        {
            try
            {
                var bids = _bidService.GetUserBidById(userId, bidId);
                if (bids == null)
                    return NotFound();
                
                var mappedBids = _mapper.Map<BidResponseModel>(bids);
                return Ok(mappedBids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
    */
}