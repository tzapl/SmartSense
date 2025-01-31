namespace SmartSense.API.ServiceModule.DTO
{
    public class RemovePeripheriesRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string HandlePub { get; set; } = string.Empty;
        public List<PeripheryAddItem>? PeripheriesToRemove { get; set; }
    }
}
