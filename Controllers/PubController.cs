using Microsoft.AspNetCore.Mvc;

namespace SmartSense.API.ServiceModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageLogger _logger;
        private readonly DataContext _context;
        private readonly IPubService _pubService;

        private readonly IPubApiInfoRepository pubApiInfoRepository;
        private readonly IPubRepository pubRepository;
        private readonly IPubRequestRepository pubRequestRepository;

        public PubController(IConfiguration configuration, IMessageLogger logger,
            DataContext context, IPubService pubService)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _pubService = pubService;

            pubApiInfoRepository = new PubApiInfoRepository(_context);
            pubRepository = new PubRepository(_context);
            pubRequestRepository = new PubRequestRepository(_context);
        }

        /// <summary>
        /// Get list of all pubs
        /// </summary>
        /// <param name="request">Set sessionId (from login)</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("GetPubList")]
        public Response<PubList?> GetPubList([FromBody] SessionRequest request)
        {
            return _pubService.GetPubList(_configuration, _logger, pubApiInfoRepository, request.SessionId);
        }

        /// <summary>
        /// Return detail of requested pub
        /// </summary>
        /// <param name="request">Set sessionId (from login) and id of requested pub</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("GetPubDetail")]
        public Response<PubDetail?> GetPubDetail([FromBody] PubDetailRequest request)
        {


            return _pubService.GetPubDetail(_configuration, _logger, pubRepository, pubRequestRepository, Config.SupportPhoneNumber(_configuration), request.PubId, request.SessionId);
        }
    }
}
