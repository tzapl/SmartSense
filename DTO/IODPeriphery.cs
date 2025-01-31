namespace SmartSense.API.ServiceModule.DTO
{
    public class IODPeriphery
    {
        public int Id { get; set; }
        public byte Type { get; set; }
        public string? SerialNumber { get; set; }
        public IODPeripheryState State { get; set; }
        public int? Slot { get; set; }
        public int? ParentHubId { get; set; }
        public int? RootSlot { get; set; }

        public static IODPeriphery Create(Periphery p, IODPeripheryState state) =>
            new IODPeriphery()
            {
                Id = p.Id,
                SerialNumber = p.SerialNumber,
                Type = p.Type,
                State = IODPeripheryState.NoTopologyRecieved
            };

        public static List<IODPeriphery> Create(List<Periphery> peripheries, IODPeripheryState state) =>
            peripheries.Select(p => Create(p, state)).ToList();
    }
}