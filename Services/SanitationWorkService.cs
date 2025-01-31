namespace SmartSense.API.ServiceModule.Services
{
    public interface ISanitationWorkService
    {
        Response<string> StartSanitation(IConfiguration configuration, IMessageLogger logger, ISanitationWorkRepository sanitationWorkRepository, IUserPermissionRepository userPermissionRepository, int unitId, string sessionId);
        Response<string> EndSanitation(IConfiguration configuration, IMessageLogger logger, ISanitationWorkRepository sanitationWorkRepository, IUserPermissionRepository userPermissionRepository, int unitId, string sessionId);

    }

    public class SanitationWorkService : ISanitationWorkService
    {
        public Response<string> StartSanitation(IConfiguration configuration, IMessageLogger logger, ISanitationWorkRepository sanitationWorkRepository, IUserPermissionRepository userPermissionRepository, int unitId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {

                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Sanitations))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        sanitationWorkRepository.StartSanitation(user.Id, unitId);
                        status = ResponseStatus.OK;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);
        }

        public Response<string> EndSanitation(IConfiguration configuration, IMessageLogger logger, ISanitationWorkRepository sanitationWorkRepository, IUserPermissionRepository userPermissionRepository, int unitId, string sessionId)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {

                    if (!userPermissionRepository.HasPermission(user.Id, user.Role, (byte)Permission.Sanitations))
                        status = ResponseStatus.ErrorNoPermission;
                    else
                    {
                        status = sanitationWorkRepository.EndSanitation(user.Id, unitId) ? ResponseStatus.OK : ResponseStatus.ErrorSanitationNotFound;
                    }
                }
                else
                    status = ResponseStatus.ErrorUserNotLogged;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(String.Empty, status);

        }
    }
}
