namespace SmartSense.API.ServiceModule.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class AddPictureRequest
    {
        public int ServiceId { get; set; } = 0;
        public byte Component { get; set; } = 0;
        public byte PictureType { get; set; } = 0;
        public byte[]? FileData { get; set; } = null;
        public string FileName { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
    }
}
