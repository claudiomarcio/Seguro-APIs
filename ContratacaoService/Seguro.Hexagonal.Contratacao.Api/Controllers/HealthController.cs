using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;

namespace Seguro.Hexagonal.Contratacao.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly IHealthService _healthService;

    public HealthController(IHealthService healthService)
    {
        _healthService = healthService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var status = await _healthService.GetStatusAsync();

        if (status == HealthStatus.Healthy)
            return Ok(new { status = "Healthy" });

        return StatusCode(503, new { status = "Unhealthy" });
    }
}
