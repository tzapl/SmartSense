using Microsoft.AspNetCore.Mvc;

namespace SmartSense.API.ServiceModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SanitationWorkController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageLogger _logger;
        private readonly DataContext _context;


        private readonly ISanitationWorkRepository sanitationWorkRepository;
        private readonly IUserPermissionRepository userPermissionRepository;

        private readonly ISanitationWorkService _sanitationWorkService;

        public SanitationWorkController(IConfiguration configuration, IMessageLogger logger,
            DataContext context, ISanitationWorkService sanitationWorkService)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _sanitationWorkService = sanitationWorkService;

            sanitationWorkRepository = new SanitationWorkRepository(_context);
            userPermissionRepository = new UserPermissionRepository(_context);
        }
        
        /// <summary>
        /// Start sanitation on requested unit
        /// </summary>
        /// <param name="request">Set sessionId (from login) and unit id</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("StartSanitation")]
        public Response<string> StartSanitation([FromBody] SanitationWorkRequest request)
        {
            return _sanitationWorkService.StartSanitation(_configuration, _logger, sanitationWorkRepository, userPermissionRepository, request.UnitId, request.SessionId);
        }

        /// <summary>
        /// Finish sanitation (set 'date to' to now) on requested unit
        /// </summary>
        /// <param name="request">Set sessionId (from login) and unit id</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("EndSanitation")]
        public Response<string> EndSanitation([FromBody] SanitationWorkRequest request)
        {
            return _sanitationWorkService.EndSanitation(_configuration, _logger, sanitationWorkRepository, userPermissionRepository, request.UnitId, request.SessionId);
        }
    }
}
