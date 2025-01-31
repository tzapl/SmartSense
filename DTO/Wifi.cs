namespace SmartSense.API.ServiceModule.DTO
{
    public class Wifi
    {
        public string SSID { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Signal { get; set; }
    }
}