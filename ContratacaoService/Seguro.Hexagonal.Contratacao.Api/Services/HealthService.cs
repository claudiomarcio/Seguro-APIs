using Microsoft.Extensions.Diagnostics.HealthChecks;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;

namespace Seguro.Hexagonal.Contratacao.Api.Services;

public class HealthService : IHealthService
{
    private readonly HealthCheckService _healthCheckService;

    public HealthService(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    public async Task<HealthStatus> GetStatusAsync()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        return report.Status;
    }
}
