﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Cqrs.Template.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.Template.Api.Controllers.V1;

[ApiVersion("1")]
[ApiController]
public class InitialController : BaseController
{
    public InitialController(INotificationHandler<ExceptionNotification> notifications) : base(notifications)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<string>), 200)]
    public Task<IActionResult> Sample()
    {
        var ipsum = new List<string> { "Nothing", "Here", "Just", "Hello" };

        return Task.FromResult(Response(Ok(new { ipsum })));
    }
}