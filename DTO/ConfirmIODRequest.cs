namespace SmartSense.API.ServiceModule.DTO
{
    public class ConfirmIODRequest : SessionRequest
    {
        public string DeviceId { get; set; } = string.Empty;
        public DateTime From { get; set; }
    }
}