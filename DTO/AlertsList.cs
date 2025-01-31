namespace SmartSense.API.ServiceModule.DTO
{
    public class AlertsList
    {
        public List<AlertItem>? List { get; set; }
    }

    public class AlertItem
    {
        public DateTime Date { get; set; }
        public string? SerialNumber { get; set; }
        public byte ErrorType { get; set; }
        public string? ErrorTypeDescription { get; set; }
        public string? Description { get; set; }
        public byte State { get; set; }
        public int? PeripheryId { get; set; }
        public byte Type { get; set; }
    }
}
