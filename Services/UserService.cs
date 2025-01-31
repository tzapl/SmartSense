using SmartSense.API.ServiceModule.Helpers;
using Resx = SmartSense.API.Base.Resources;

namespace SmartSense.API.ServiceModule.Services
{
    public interface IUserService
    {
        Response<UserLoginResponse> Login(IConfiguration configuration, IMessageLogger logger, IUserRepository userRepository, string login, string password, string restApiAddress, string restApiToken);
        Response<string> Logout(IConfiguration configuration, IMessageLogger logger, string sessionId, string userToken, string restApiAddress, string restApiToken);
        Response<Permissions?> GetPermissions(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, string sessionId);
        Response<string> ResetUserPassword(IConfiguration configuration, IMessageLogger logger, IUserRepository userRepository, IEmailRepository emailRepository, string login, string email, string from, string fromName);
        Response<string> ResetSelfPassword(IConfiguration configuration, IMessageLogger logger, IUserRepository userRepository, IEmailRepository emailRepository, string sessionId, string from, string fromName);
    }

    public class UserService : IUserService
    {
        public Response<UserLoginResponse> Login(IConfiguration configuration, IMessageLogger logger, IUserRepository userRepository, string login, string password, string restApiAddress, string restApiToken)
        {
            var status = ResponseStatus.ErrorUnknown;
            var response = new UserLoginResponse();
            var userGuid = string.Empty;

            try
            {
                var user = userRepository.GetByLogin(login);
                if (user == null)
                    status = ResponseStatus.ErrorUserDoesNotExists;
                else
                {
                    if (user.Status == (byte)UserStatus.Active &&
                        user.State == (byte)UserState.Active &&
                        Config.LoginRoles.Contains((UserRole)user.Role) &&
                        user.Password == Utility.HashPassword(password))
                    {
                        if (String.IsNullOrEmpty(user.Handle))
                            status = ResponseStatus.ErrorUserNotLogged;
                        else
                        {
                            //get user token
                            try
                            {
                                var userToken = RestApiHelper.GetUserTokenAsync(user.Handle, restApiToken, restApiAddress);
                                userGuid = $"{Guid.NewGuid()}";
                                new SessionManager().Add(userGuid, user);
                                status = ResponseStatus.OK;

                                response.UserGuid = userGuid;
                                response.UserToken = userToken;
                                response.Handle = user.Handle;
                            }
                            catch
                            {
                                status = ResponseStatus.ErrorUserNotLogged;
                            }
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<UserLoginResponse>(response, status);
        }

        public Response<string> Logout(IConfiguration configuration, IMessageLogger logger, string sessionId, string userToken, string restApiAddress, string restApiToken)
        {
            var exists = false;
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                exists = sessionManager.Exists(sessionId, out _);
                status = exists ? ResponseStatus.OK : ResponseStatus.ErrorUnknown;
                sessionManager.Remove(sessionId);

                //RestApiHelper.DeleteToken(userToken, restApiToken, restApiAddress);
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty,
                status);
        }

        public Response<Permissions?> GetPermissions(IConfiguration configuration, IMessageLogger logger, IUserPermissionRepository userPermissionRepository, string sessionId)
        {
            Permissions? permissions = null;
            var status = ResponseStatus.ErrorUnknown;
            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    permissions = userPermissionRepository.GetPermissions(user.Id, user.Role);
                    status = permissions != null ? ResponseStatus.OK : ResponseStatus.ErrorUnknown;
                }
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<Permissions?>(permissions, status);
        }

        public Response<string> ResetSelfPassword(IConfiguration configuration, IMessageLogger logger, IUserRepository userRepository, IEmailRepository emailRepository, string sessionId, string from, string fromName)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    var newPassword = Utility.GeneratePassword(8);
                    var userDB = userRepository.GetById(user.Id);
                    if (userDB != null)
                    {
                        userRepository.SetUserPassword(userDB, newPassword);
                        var name = userDB.Firstname + " " + userDB.Surname;
                        SendResetPassword(emailRepository, userDB.Email, name, userDB.Login, newPassword, userDB.Culture, from, fromName);
                        status = ResponseStatus.OK;
                    }
                    else
                        status = ResponseStatus.ErrorUserDoesNotExists;
                }
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty, status);
        }

        public Response<string> ResetUserPassword(IConfiguration configuration, IMessageLogger logger, IUserRepository userRepository, IEmailRepository emailRepository, string login, string email, string from, string fromName)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var newPassword = Utility.GeneratePassword(8);
                var userDB = userRepository.GetByLoginAndEmail(login, email);

                if (userDB != null)
                {
                    userRepository.SetUserPassword(userDB, newPassword);
                    var name = userDB.Firstname + " " + userDB.Surname;
                    SendResetPassword(emailRepository, userDB.Email, name, userDB.Login, newPassword, userDB.Culture, from, fromName);
                    status = ResponseStatus.OK;
                }
                else
                    status = ResponseStatus.ErrorUserDoesNotExists;
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty, status);
        }


        public Response<string> SetUserPassword(IConfiguration configuration, IMessageLogger logger, IUserRepository userRepository, string sessionId, string password)
        {
            var status = ResponseStatus.ErrorUnknown;

            try
            {
                var sessionManager = new SessionManager();

                if (sessionManager.Exists(sessionId, out User? user) &&
                    user != null)
                {
                    var userDB = userRepository.GetById(user.Id);
                    if (userDB != null)
                    {
                        userRepository.SetUserPassword(userDB, password);
                        status = ResponseStatus.OK;
                    }
                    else
                        status = ResponseStatus.ErrorUserDoesNotExists;
                }
            }
            catch (Exception ex)
            {
                logger.Log(configuration, ex);
                status = ResponseStatus.ErrorUnknown;
            }
            return new Response<string>(string.Empty, status);

        }

        private void SendResetPassword(IEmailRepository emailRepository, string email, string name, string login, string newPassword, byte userCulture, string from, string fromName)
        {
            var culture = (UserCulture)userCulture;
            var rm = new Resx.ResMgr("SmartSense.API.Base.Resources.User");
            var message = new Email()
            {
                To = email,
                ToName = name,
                From = from,
                FromName = fromName,
                Date = DateTime.UtcNow,
                Subject = string.Format(rm.GetString("ResetPasswordEmailSubject", culture), login),
                Body = string.Format(rm.GetString("ResetPasswordEmailText", culture), login, newPassword),
            };

            emailRepository.SendMail(message);
        }
    }
}