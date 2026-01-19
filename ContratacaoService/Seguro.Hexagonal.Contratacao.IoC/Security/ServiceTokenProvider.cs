using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Seguro.Hexagonal.Contratacao.Data.Secutiry;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Seguro.Hexagonal.Contratacao.IoC.Security;

public class ServiceTokenProvider : IServiceTokenProvider
{
    private readonly IConfiguration _configuration;

    public ServiceTokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken()
    {
        var jwt = _configuration.GetSection("Jwt");

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "ContratacaoService"),
            new Claim(ClaimTypes.Role, "Service")
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwt["Key"]!));

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials:
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
