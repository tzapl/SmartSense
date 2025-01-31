namespace SmartSense.API.ServiceModule.DTO
{
    public class AddServiceComponentsRequest
    {
        public int UnitId { get; set; } = 0;
        public List<byte> Components { get; set; } = new List<byte>();
        public string SessionId { get; set; } = string.Empty;
    }
}
