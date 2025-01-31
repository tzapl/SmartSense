namespace SmartSense.API.ServiceModule.DTO
{
    public class CheckIODRequest : SessionRequest
    {
        public string DeviceId { get; set; } = string.Empty;
        public DateTime From { get; set; }
    }
}