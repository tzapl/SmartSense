namespace SmartSense.API.ServiceModule.DTO
{
    public class StartServiceRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string TaskHandle { get; set; } = string.Empty;
        public string UserToken { get; set; } = string.Empty;
    }
}
