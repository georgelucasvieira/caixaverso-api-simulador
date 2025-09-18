using ApiSimulador.Application.Common.Base;

namespace ApiSimulador.Domain.Exceptions;

public class ForbiddenException : BaseException
{
    public override int ErrorCode => 403;

    public override string DeveloperMessage => "Access to the resource is forbidden.";

    public ForbiddenException(string message) : base(message)
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}