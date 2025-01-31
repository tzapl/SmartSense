namespace SmartSense.API.ServiceModule.DTO
{
    public class WifiListRequest : SessionRequest
    {
        public string DeviceId { get; set; } = string.Empty;
    }
}