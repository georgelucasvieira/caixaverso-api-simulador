namespace ApiSimulador.Application.Common.Base;

public class BaseExceptionDetails
{
    public int ErrorCode { get; set; } = 500;
    public string Message { get; set; } = "An error occurred while processing your request.";
    public string DeveloperMessage { get; set; } = "An error occurred while processing your request.";
    public string Timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");
    public object? Errors { get; set; }

    public static BaseExceptionDetails From(BaseException exception, object? errors)
    {
        return new BaseExceptionDetails
        {
            ErrorCode = exception.ErrorCode,
            Message = exception.Message,
            DeveloperMessage = exception.DeveloperMessage,
            Timestamp = exception.Timestamp,
            Errors = errors
        };
    }

    public static BaseExceptionDetails Generic()
    {
        return new BaseExceptionDetails();
    }

    public static BaseExceptionDetails Generic(string message)
    {
        return new BaseExceptionDetails {
            Message = message
        };
    }
}