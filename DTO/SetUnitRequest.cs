namespace SmartSense.API.ServiceModule.DTO
{
    public class SetUnitRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string HandlePub { get; set; } = string.Empty;
        public string SetCode { get; set; } = string.Empty;
        public string NFC { get; set; } = string.Empty;
        public bool HasSimcard { get; set; } = false;
        public List<PeripheryAddItem>? Peripheries { get; set; }
    }

    public class PeripheryAddItem
    {
        public byte Type { get; set; }
        public string? SerialNumber { get; set; }
        public int? PressureSensorId { get; set; }
    }
}
