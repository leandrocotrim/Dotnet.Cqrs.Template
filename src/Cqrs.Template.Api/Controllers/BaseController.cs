using System.Collections.Generic;
using Cqrs.Template.Api.Dtos;
using Cqrs.Template.Api.Filters;
using Cqrs.Template.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.Template.Api.Controllers;

[Route("your-project-name/[controller]/v{version:apiVersion}")]
[ServiceFilter(typeof(GlobalExceptionFilterAttribute))]
public class BaseController : Controller
{
    private readonly ExceptionNotificationHandler _notifications;

    protected IEnumerable<ExceptionNotification> Notifications => _notifications.GetNotifications();

    protected BaseController(INotificationHandler<ExceptionNotification> notifications)
    {
        _notifications = (ExceptionNotificationHandler)notifications;
    }

    protected bool IsValidOperation()
    {
        return !_notifications.HasNotifications();
    }

    protected new IActionResult Response(IActionResult action)
    {
        if (!IsValidOperation())
        {
            return BadRequest(new Response<object>(
                _notifications.GetNotifications())
            );
        }

        return action;
    }
}
