namespace SmartSense.API.ServiceModule.DTO
{
    public enum IODPeripheryState : byte
    {
        OK = 0,

        NotActive = 1,
        NotFunctional = 2,
        NoTopologyRecieved = 3,

        Unknown = byte.MaxValue
    }
}