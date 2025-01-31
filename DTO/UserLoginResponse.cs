namespace SmartSense.API.ServiceModule.DTO
{
    public class UserLoginResponse
    {
        public string UserToken { get; set; } = string.Empty;
        public string UserGuid { get; set; } = string.Empty;
        public string Handle { get; set; } = string.Empty;
    }
}