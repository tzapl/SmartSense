namespace SmartSense.API.ServiceModule.DTO
{
    public class TopologyData
    {
        public List<TopologyItem> Hubs { get; set; } = new List<TopologyItem>();
        public List<TopologyItem> Peripheries { get; set; } = new List<TopologyItem>();
    }

    public class TopologyItem
    {
        public string SerialNumber { get; private set; }
        public byte PeripheryType { get; private set; }
        public int ParentHubId { get; private set; }
        public int Slot { get; private set; }

        public TopologyItem(string serialNumber, byte peripheryType, int parentHubId, int slot)
        {
            SerialNumber = serialNumber;
            PeripheryType = peripheryType;
            ParentHubId = parentHubId;
            Slot = slot;
        }
    }
}