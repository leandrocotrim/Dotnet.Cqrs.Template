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

        var properties = section.GetType().GetProperties();

        var property = properties.FirstOrDefault(x => x.GetValue(section) == null);

        return Task.FromResult(
            property == null
                ? HealthCheckResult.Healthy($"Section {context.Registration.Name} está configurada corretamente")
                : HealthCheckResult.Unhealthy($"Section {context.Registration.Name} está faltando o campo: {property.Name}")
        );
    }
}
