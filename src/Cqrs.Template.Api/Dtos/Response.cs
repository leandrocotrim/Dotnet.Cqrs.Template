using System.Collections.Generic;
using Cqrs.Template.Domain.Exceptions;

namespace Cqrs.Template.Api.Dtos;

public class Response<T>
{
    public bool Success { get; }
    public T Data { get; }
    public List<ExceptionNotification> Errors { get; }

    public Response(T data)
    {
        Success = true;
        Data = data;
    }

    public Response(List<ExceptionNotification> errors)
    {
        Success = false;
        Errors = errors;
    }
}
