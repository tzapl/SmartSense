namespace SmartSense.API.ServiceModule.DTO
{
    public class GetUnitPeripheriesDataRequest
    {
        public int UnitId { get; set; }
        public DateTime From { get; set; }
        public string SessionId { get; set; } = String.Empty;
    }
}
