using ApiSimulador.Application.Common.Base;

namespace ApiSimulador.Domain.Exceptions;

public class DuplicateKeyException : BaseException
{
    public override int ErrorCode => 409;

    public override string DeveloperMessage => "An element with the specified key already exists.";

    public DuplicateKeyException(string message) : base(message)
    {
    }

    public DuplicateKeyException(string message, Exception innerException) : base(message, innerException)
    {
    }
}