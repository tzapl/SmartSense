namespace SmartSense.API.ServiceModule.DTO
{
    public enum ResponseStatus : int
    {
        OK = 0,
        ApiResponseOK = 200,
        ResponseCreated = 201,

        ErrorUserDoesNotExists = 1,

        ErrorUnknown = 100_000,
        ErrorApiKey = 100_001,
        ErrorUserNotLogged = 100_002,
        ErrorNoPermission = 100_003,

        ErrorUnitNotFound = 100_021,
        ErrorUnitAlreadyInMontage = 100_022,
        ErrorUnitDuplicity = 100_023,


        ErrorPubNotFound = 100_031,
        ErrorServiceNotFound = 100_041,
        ErrorSrviceNoteOutOfRange = 100_042,

        ErrorPeripheryNotFound = 100_051,

        ErrorNoData = 100_060,

        ErrorSanitationNotFound = 100_071,
        ErrorNoFileData = 100_080,
    }

    public class Response<T>
    {
        public T? Data { get; set; }
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }

        public Response(T? data, ResponseStatus status) : this(data, status, string.Empty) { }
        public Response(T? data, ResponseStatus status, string message)
        {
            Data = data;
            Status = status;
            Message = message;
        }
    }
}