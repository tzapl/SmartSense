namespace SmartSense.API.ServiceModule.DTO
{
    public class CheckIODResponse
    {
        public List<IODPeriphery> Peripheries { get; set; } = new List<IODPeriphery>();
        public DateTime? LastDataRecieved { get; set; }

        public bool IsValid()
        {
            bool isValid = true;
            foreach (var p in Peripheries)
                isValid &= p.State == IODPeripheryState.OK;
            return isValid;
        }

        public void ConvertPeripheriesToExternal()
        {
            foreach (var p in Peripheries)
                p.Type = PeripheryConvertor.ToExternal(p.Type);
        }
    }
}