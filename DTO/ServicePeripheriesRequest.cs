namespace SmartSense.API.ServiceModule.DTO
{
    public class ServicePeripheriesRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string HandlePub { get; set; } = string.Empty;
        public List<byte>? Peripheries { get; set; }
    }
}
