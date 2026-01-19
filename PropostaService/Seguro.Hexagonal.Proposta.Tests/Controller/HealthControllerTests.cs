using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;
using Seguro.Hexagonal.Proposta.Api.Controllers;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Services;

namespace Seguro.Hexagonal.Proposta.Api.Tests;

public class HealthControllerTests
{
    private readonly Mock<IHealthService> _healthServiceMock;
    private readonly HealthController _controller;

    public HealthControllerTests()
    {
        _healthServiceMock = new Mock<IHealthService>();
        _controller = new HealthController(_healthServiceMock.Object);
    }

    [Fact]
    public async Task GetAsync_Deve_retornar_200_quando_healthy()
    {
        _healthServiceMock
            .Setup(x => x.GetStatusAsync())
            .ReturnsAsync(HealthStatus.Healthy);

        var result = await _controller.GetAsync();

        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, ok.StatusCode);
    }

    [Fact]
    public async Task GetAsync_Deve_retornar_503_quando_unhealthy()
    {
        _healthServiceMock
            .Setup(x => x.GetStatusAsync())
            .ReturnsAsync(HealthStatus.Unhealthy);

        var result = await _controller.GetAsync();

        var status = Assert.IsType<ObjectResult>(result);
        Assert.Equal(503, status.StatusCode);
    }
}
