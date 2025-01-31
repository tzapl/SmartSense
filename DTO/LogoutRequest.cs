namespace SmartSense.API.ServiceModule.DTO
{
    public class LogoutRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string UserToken { get; set; } = string.Empty;
    }
}