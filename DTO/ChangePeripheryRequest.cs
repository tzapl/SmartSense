namespace SmartSense.API.ServiceModule.DTO
{
    public class ChangePeripheryRequest
    {
        public int PeripheryId { get; set; } = 0;
        public string SerialNumber { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
    }
}
