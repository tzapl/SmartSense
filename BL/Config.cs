namespace SmartSense.API.ServiceModule.BL
{
    public class Config
    {
        public static HashSet<UserRole> LoginRoles =>
            new HashSet<UserRole>() { UserRole.SuperAdmin, UserRole.SanitationOperator, UserRole.ServiceOperator };

        public static string ApiKey(IConfiguration configuration) =>
            configuration["ApiKey"] != null ? configuration["ApiKey"] : string.Empty;
        public static bool CheckApiKey(IConfiguration configuration, string apiKey) =>
            ApiKey(configuration) == apiKey;
        public static int UnitLowIntervalValue(IConfiguration configuration) =>
            configuration["UnitLowIntervalValue"] != null ? Convert.ToInt32(configuration["UnitLowIntervalValue"]) : 0;
        public static int UnitNormalIntervalValue(IConfiguration configuration) =>
            configuration["UnitNormalIntervalValue"] != null ? Convert.ToInt32(configuration["UnitNormalIntervalValue"]) : 0;
        public static string SupportPhoneNumber(IConfiguration configuration) =>
            configuration["SupportPhoneNumber"] != null ? configuration["SupportPhoneNumber"] : string.Empty;
        public static string EmailFrom(IConfiguration configuration) =>
            configuration["EmailFrom"] != null ? configuration["EmailFrom"] : string.Empty;
        public static string EmailFromName(IConfiguration configuration) =>
            configuration["EmailFromName"] != null ? configuration["EmailFromName"] : string.Empty;
        public static string RestApiToken(IConfiguration configuration) =>
            configuration["RestApiToken"] != null ? configuration["RestApiToken"] : string.Empty;
        public static string RestApiAddress(IConfiguration configuration) =>
            configuration["RestApiAddress"] != null ? configuration["RestApiAddress"] : string.Empty;
        public static int UnitDefaultSanitationLimit(IConfiguration configuration) =>
            configuration["UnitDefaultSanitationLimit"] != null ? Convert.ToInt32(configuration["UnitDefaultSanitationLimit"]) : 0;
        public static int UnitDefaultSanitationSpongesLimit(IConfiguration configuration) =>
        configuration["UnitDefaultSanitationSpongesLimit"] != null ? Convert.ToInt32(configuration["UnitDefaultSanitationSpongesLimit"]) : 0;
        public static int UnitDefaultFlushLimit(IConfiguration configuration) =>
        configuration["UnitDefaultFlushLimit"] != null ? Convert.ToInt32(configuration["UnitDefaultFlushLimit"]) : 0;
        public static int UnitDefaultWaterLevel(IConfiguration configuration) =>
        configuration["UnitDefaultWaterLevel"] != null ? Convert.ToInt32(configuration["UnitDefaultWaterLevel"]) : 0;
        public static string TapGeneratedName(IConfiguration configuration) =>
        configuration["TapGeneratedName"] != null ? configuration["TapGeneratedName"] : string.Empty;
        public static int PubSkladId(IConfiguration configuration) =>
        configuration["PubSkladId"] != null ? Convert.ToInt32(configuration["PubSkladId"]) : 0;

    }
}