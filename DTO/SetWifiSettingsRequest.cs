namespace SmartSense.API.ServiceModule.DTO
{
    public class SetWifiSettingsRequest
    {
        public string SessionId { get; set; } = string.Empty;
        public string DeviceId { get; set; } = string.Empty;
        public bool? Wifi { get; set; }
        public string? WifiSSID { get; set; }
        public string? Password { get; set; }
    }
}
