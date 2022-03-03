using System;
using Cqrs.Template.Api.Filters.ErrorsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cqrs.Template.Api.Filters;

public class GlobalExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public GlobalExceptionFilterAttribute()
    {
    }

    public void OnException(ExceptionContext context)
    {
        context.Result = new BadRequestObjectResult(
            new DefaultError(false,
                new[]
                {
                    new ErrorsResponse(Environment.GetEnvironmentVariable("GlobalErrorCode"),
                        Environment.GetEnvironmentVariable("GlobalErrorMessage"),
                        DateTime.Now)
                }
            )
        );
    }
}