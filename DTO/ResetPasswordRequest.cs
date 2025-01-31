namespace SmartSense.API.ServiceModule.DTO
{
    public class ResetPasswordRequest
    {
        public string ApiKey { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
