using System;
using Cqrs.Template.Api.Filters.ErrorsModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Cqrs.Template.Api.Filters;

public class GlobalExceptionFilterAttribute : Attribute, IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilterAttribute> _logger;

    public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var eventId = new EventId(188, "GlobalException");

        _logger.LogError(eventId, context.Exception, context.Exception.Message);

        var errorMessage = new DefaultError(false,
            new[]
            {
                new ErrorsResponse(
                    Environment.GetEnvironmentVariable("GlobalErrorCode"),
                    Environment.GetEnvironmentVariable("GlobalErrorMessage"),
                    context.HttpContext.Request.Path,
                    StatusCodes.Status400BadRequest
                )
            }
        );

        context.Result = new BadRequestObjectResult(errorMessage);
    }
}
