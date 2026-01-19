using Seguro.Hexagonal.Contratacao.Data.Secutiry;
using Seguro.Hexagonal.Contratacao.Domain.Enums;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;
using Seguro.Hexagonal.Contratacao.Domain.ValueObjects;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Seguro.Hexagonal.Contratacao.Data.HttpClients;

public class PropostaServiceClient : IPropostaServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly IServiceTokenProvider _tokenProvider;

    public PropostaServiceClient(
        HttpClient httpClient,
        IServiceTokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
    }

    public async Task<PropostaStatusResult> ObterStatusAsync(Guid propostaId)
    {
        var token = _tokenProvider.GenerateToken();

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync($"/api/propostas/{propostaId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return new PropostaStatusResult(
                propostaId,
                PropostaSituacao.NaoEncontrada);
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Erro ao consultar proposta {propostaId}");
        }

        var proposta = await response.Content.ReadFromJsonAsync<PropostaDto>();

        return proposta!.Status switch
        {
            "Aprovada" => new PropostaStatusResult(
                propostaId,
                PropostaSituacao.Aprovada),

            _ => new PropostaStatusResult(
                propostaId,
                PropostaSituacao.NaoAprovada)
        };
    }

    private sealed class PropostaDto
    {
        public string Status { get; init; } = string.Empty;
    }
}
