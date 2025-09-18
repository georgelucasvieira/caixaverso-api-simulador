using ApiSimulador.Application.Common.Base;

namespace ApiSimulador.Domain.Exceptions;

public class NotFoundException : BaseException 
{
    public override int ErrorCode => 404;

    public override string DeveloperMessage => "The requested resource was not found.";
    
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}