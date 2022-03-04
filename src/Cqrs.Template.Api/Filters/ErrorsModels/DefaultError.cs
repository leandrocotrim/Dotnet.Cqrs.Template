using System;

namespace Cqrs.Template.Api.Filters.ErrorsModels;

public class DefaultError
{
    public bool Success { get; }
    public ErrorsResponse[] Errors { get; }

    public DefaultError(bool success, ErrorsResponse[] errors)
    {
        Success = success;
        Errors = errors;
    }
}

public class ErrorsResponse
{
    public string Code { get; }
    public string Message { get; }
    public string Instance { get; }
    public int Status { get; }
    public DateTime TimeStamp { get; }

    public ErrorsResponse(string code, string message, string instance, int status)
    {
        Code = code;
        Message = message;
        Instance = instance;
        Status = status;
        TimeStamp = DateTime.UtcNow;
    }
}
