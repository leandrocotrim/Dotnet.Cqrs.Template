﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Cqrs.Template.Infra.CrossCutting.IoC.Configurations.HealthCheck;

internal class RequiredSectionsHealthCheck<T> : IHealthCheck
{
    private readonly IConfiguration _configuration;

    public RequiredSectionsHealthCheck(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var section = _configuration.GetSection(context.Registration.Name).Get<T>();

        if (section is null) return Task.FromResult(HealthCheckResult.Unhealthy($"Section {context.Registration.Name} not mapped"));

        var properties = section.GetType().GetProperties();

        var missingProperties = properties.Where(x => x.GetValue(section) is null).ToList();

        IReadOnlyDictionary<string, object> data = new Dictionary<string, object>
        {
            { "MissingProperties", string.Join(", ", missingProperties) }
        };

        return Task.FromResult(
            !missingProperties.Any()
                ? HealthCheckResult.Healthy($"Section {context.Registration.Name} mapped")
                : HealthCheckResult.Unhealthy($"Section {context.Registration.Name} not properly mapped, missing fields", data: data)
        );
    }
}
