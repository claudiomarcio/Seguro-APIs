using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;

public interface IHealthService
{
    Task<HealthStatus> GetStatusAsync();
}