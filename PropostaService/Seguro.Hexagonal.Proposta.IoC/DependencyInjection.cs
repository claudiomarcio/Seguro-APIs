using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seguro.Hexagonal.Application.UseCases;
using Seguro.Hexagonal.Data.Context;
using Seguro.Hexagonal.Data.Repositories;
using Seguro.Hexagonal.Proposta.Application.Interfaces;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;

namespace Seguro.Hexagonal.Proposta.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddHexagonalIoC(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // -----------------------
        // EF Core - SQL Server
        // -----------------------
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // -----------------------
        // Repositories
        // -----------------------
        services.AddScoped<IPropostaRepository, PropostaRepositoryEf>();

        // -----------------------
        // Use Cases
        // -----------------------
        services.AddScoped<ICriarPropostaUseCase, CriarPropostaUseCase>();
        services.AddScoped<IListarPropostasUseCase, ListarPropostasUseCase>();
        services.AddScoped<IAlterarStatusPropostaUseCase, AlterarStatusPropostaUseCase>();

        return services;
    }
}
