using ApiSimulador.Application.Common.Base;

namespace ApiSimulador.Domain.Exceptions;

public class UnauthorizedException : BaseException
{
    public override int ErrorCode => 401;

    public override string DeveloperMessage => "Unauthorized access to the resource.";

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}