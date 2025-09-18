namespace ApiSimulador.Application.Common.Base;

public abstract class BaseException : Exception
{
    public abstract int ErrorCode { get; }
    public abstract string DeveloperMessage { get; }
    public string Timestamp { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK");

    public BaseException(string message) : base(message)
    {
    }

    public BaseException(string message, Exception innerException) : base(message, innerException)
    {
    }
}