﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cqrs.Template.Infra.CrossCutting.IoC.Configurations.HealthCheck;

internal class RequiredVariablesHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var variable = Environment.GetEnvironmentVariable(context.Registration.Name);

        if (!string.IsNullOrWhiteSpace(variable))
            return Task.FromResult(HealthCheckResult.Healthy("Mapped variable"));

        return Task.FromResult(HealthCheckResult.Unhealthy($"Variable not mapped: {context.Registration.Name}"));
    }
}
