namespace SmartSense.API.ServiceModule.DTO
{
    public class ServiceNoteRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
