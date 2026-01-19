using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seguro.Hexagonal.Contratacao.Application.UseCases;
using Seguro.Hexagonal.Contratacao.Data.Context;
using Seguro.Hexagonal.Contratacao.Data.HttpClients;
using Seguro.Hexagonal.Contratacao.Data.Repositories;
using Seguro.Hexagonal.Contratacao.Data.Secutiry;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Repositories;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.UseCases;
using Seguro.Hexagonal.Contratacao.IoC.Resilience;
using Seguro.Hexagonal.Contratacao.IoC.Security;

namespace Seguro.Hexagonal.Contratacao.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddContratacaoIoC(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // -----------------------
        // EF Core - SQL Server
        // -----------------------
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("ContratacaoConnection")));

        // -----------------------
        // Repositories
        // -----------------------
        services.AddScoped<IContratacaoRepository, ContratacaoRepositoryEf>();

        // -----------------------
        // HTTP Client - PropostaService
        // -----------------------
        services.AddHttpClient<IPropostaServiceClient, PropostaServiceClient>(client =>
        {
            client.BaseAddress = new Uri(
                configuration["Services:PropostaService"]);
        })
        .AddPolicyHandler(HttpPolicies.RetryPolicy)
        .AddPolicyHandler(HttpPolicies.TimeoutPolicy);

        //----------------------
        // Security
        //----------------------
        services.AddSingleton<IServiceTokenProvider, ServiceTokenProvider>();

        // -----------------------
        // Use Cases
        // -----------------------
        services.AddScoped<IContratarPropostaUseCase, ContratarPropostaUseCase>();

        return services;
    }
}
