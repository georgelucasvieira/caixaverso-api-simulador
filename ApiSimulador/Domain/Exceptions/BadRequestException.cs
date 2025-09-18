using ApiSimulador.Application.Common.Base;

namespace ApiSimulador.Domain.Exceptions;

public class BadRequestException : BaseException
{
    public override int ErrorCode => 400;

    public override string DeveloperMessage => "The request was invalid or cannot be served.";

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}