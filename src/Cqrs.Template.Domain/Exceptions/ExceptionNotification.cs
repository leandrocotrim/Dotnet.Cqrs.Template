using Cqrs.Template.Domain.Events;

namespace Cqrs.Template.Domain.Exceptions;

public class ExceptionNotification : Event
{
    public string Code { get; }
    public string Message { get; }
    public string ParamName { get; }

    public ExceptionNotification(string code, string message, string paramName = null)
    {
        Code = code;
        Message = message;
        ParamName = paramName;
    }
}