namespace SmartSense.API.ServiceModule.DTO
{
    public class ConfirmIODResponse
    {
        public bool Success { get; set; }
        public CheckIODResponse? CheckResponse { get; set; }
    }
}