namespace SmartSense.API.ServiceModule.DTO
{
    public class UserLoginRequest
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}