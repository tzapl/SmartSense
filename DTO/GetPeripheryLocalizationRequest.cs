namespace SmartSense.API.ServiceModule.DTO
{
    public class GetPeripheryLocalizationRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public int PeripheryId { get; set; } = 0;
    }
}
