namespace SmartSense.API.ServiceModule.DTO
{
    public class UnitDetail
    {
        public int Id { get; set; }
        public string DeviceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int? PubId { get; set; }
        public List<PeripheryItem>? Peripheries { get; set; }
        public bool HasUnit { get; set; }
    }

    public class PeripheryItem
    {
        public int Id { get; set; }
        public byte Type { get; set; }
        public string? SerialNumber { get; set; }
        public int? PressureSensorId { get; set; }
        public int? Order { get; set; }
    }
}
