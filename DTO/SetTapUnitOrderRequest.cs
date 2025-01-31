namespace SmartSense.API.ServiceModule.DTO
{
    public class SetTapUnitOrderRequest
    {
        public int UnitId { get; set; }
        public int PeripheryId { get; set; }
        public int Order { get; set; }
        public string SessionId { get; set; } = string.Empty;
    }
}
