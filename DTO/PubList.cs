namespace SmartSense.API.ServiceModule.DTO
{
    public class PubList
    {
        public List<PubItem>? Pubs { get; set; }
    }

    public class PubItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Contact { get; set; }
        public bool MontageRequest { get; set; }
    }
}
