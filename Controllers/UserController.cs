using Microsoft.AspNetCore.Mvc;

namespace SmartSense.API.ServiceModule.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageLogger _logger;
        private readonly DataContext _context;
        private readonly IUserService _userService;

        private readonly IUserRepository userRepository;
        private readonly IEmailRepository emailRepository;
        private readonly IUserPermissionRepository userPermissionRepository;

        public UserController(IConfiguration configuration, IMessageLogger logger,
            DataContext context, IUserService userService)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _userService = userService;

            userRepository = new UserRepository(_context);
            userPermissionRepository = new UserPermissionRepository(_context);
            emailRepository = new EmailRepository(_context);
        }

        /// <summary>
        /// Log user into api and return session id for another communication with api and token for communicaton with external api
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Set api key, login and password</returns>
        [HttpPost("[action]"), ActionName("Login")]
        public Response<UserLoginResponse> Login([FromBody] UserLoginRequest request)
        {
            if (!Config.CheckApiKey(_configuration, request.ApiKey))
                return new Response<UserLoginResponse>(new UserLoginResponse(), ResponseStatus.ErrorApiKey);

            return _userService.Login(_configuration, _logger, userRepository, request.Login, request.Password, Config.RestApiAddress(_configuration), Config.RestApiToken(_configuration));
        }

        /// <summary>
        /// Destroy session id in api and also destroy token in external api
        /// </summary>
        /// <param name="request">Set sessionId (from login) and token for external api</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("Logout")]
        public Response<string> Logout([FromBody] LogoutRequest request) =>
            _userService.Logout(_configuration, _logger, request.SessionId, request.UserToken, Config.RestApiAddress(_configuration), Config.RestApiToken(_configuration));

        /// <summary>
        /// Return list of permissions for logged user
        /// </summary>
        /// <param name="request">Set sessionId (from login)</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("GetPermissions")]
        public Response<Permissions?> GetPermissions([FromBody] SessionRequest request) =>
            _userService.GetPermissions(_configuration, _logger, userPermissionRepository, request.SessionId);

        /// <summary>
        /// Reset user´s password and send it to e-mail
        /// </summary>
        /// <param name="request">Set api key, login and e-mail</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("ResetPassword")]
        public Response<string> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!Config.CheckApiKey(_configuration, request.ApiKey))
                return new Response<string>(string.Empty, ResponseStatus.ErrorApiKey);

            return _userService.ResetUserPassword(_configuration, _logger, userRepository, emailRepository, request.Login, request.Email, Config.EmailFrom(_configuration), Config.EmailFromName(_configuration));
        }

        /// <summary>
        /// Reset password for logged user and send it to e-mail
        /// </summary>
        /// <param name="request">Set sessionId (from login)</param>
        /// <returns></returns>
        [HttpPost("[action]"), ActionName("ResetSelfPassword")]
        public Response<string> ResetSelfPassword([FromBody] SessionRequest request) =>
            _userService.ResetSelfPassword(_configuration, _logger, userRepository, emailRepository, request.SessionId, Config.EmailFrom(_configuration), Config.EmailFromName(_configuration));
    }
}
