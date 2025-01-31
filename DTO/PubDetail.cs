namespace SmartSense.API.ServiceModule.DTO
{
    public class PubDetail
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool MontageRequest { get; set; }
        public string? SupportPhoneNumber { get; set; }
    }
}
