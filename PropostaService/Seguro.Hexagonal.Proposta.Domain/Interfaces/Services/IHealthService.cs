using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Seguro.Hexagonal.Proposta.Domain.Interfaces.Services;

public interface IHealthService
{
    Task<HealthStatus> GetStatusAsync();
}