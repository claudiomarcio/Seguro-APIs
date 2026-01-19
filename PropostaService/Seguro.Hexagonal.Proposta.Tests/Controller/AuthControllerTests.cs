using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Seguro.Hexagonal.Proposta.Api.Controllers;

namespace Seguro.Hexagonal.Proposta.Api.Tests;

public class AuthControllerTests
{
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Jwt:Key", "TEST_KEY_123456789012345678901234" },
            { "Jwt:Issuer", "Seguro.Hexagonal" },
            { "Jwt:Audience", "Seguro.Hexagonal.Clients" },
            { "Jwt:ExpireMinutes", "60" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build();

        _controller = new AuthController(configuration);
    }

    [Fact]
    public void GerarToken_Deve_retornar_200_com_token()
    {
        // Act
        var result = _controller.GerarToken();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);

        var response = okResult.Value!.GetType().GetProperty("token")?.GetValue(okResult.Value);
        Assert.NotNull(response);
        Assert.False(string.IsNullOrWhiteSpace(response.ToString()));
    }
}
