using Microsoft.Extensions.Logging;
using Seguro.Hexagonal.Contratacao.Domain.Commands;
using Seguro.Hexagonal.Contratacao.Domain.Enums;
using Seguro.Hexagonal.Contratacao.Domain.Exceptions;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Repositories;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.Services;
using Seguro.Hexagonal.Contratacao.Domain.Interfaces.UseCases;
using Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

namespace Seguro.Hexagonal.Contratacao.Application.UseCases;

public class ContratarPropostaUseCase : IContratarPropostaUseCase
{
    private readonly IPropostaServiceClient _propostaServiceClient;
    private readonly IContratacaoRepository _contratacaoRepository;
    private readonly ILogger<ContratarPropostaUseCase> _logger;
    public ContratarPropostaUseCase(
        IPropostaServiceClient propostaServiceClient,
        IContratacaoRepository contratacaoRepository,
        ILogger<ContratarPropostaUseCase> logger)
    {
        _propostaServiceClient = propostaServiceClient;
        _contratacaoRepository = contratacaoRepository;
        _logger = logger;
    }

    public async Task<ContratarPropostaResult> ExecuteAsync(ContratarPropostaCommand command)
    {
        _logger.LogInformation(
           "Iniciando contratação da proposta {PropostaId}",
           command.PropostaId);

        if (command.PropostaId == Guid.Empty)
            throw new DomainException("PropostaId inválido.");


        var status = await _propostaServiceClient.ObterStatusAsync(command.PropostaId);

        if (status.Situacao == PropostaSituacao.NaoEncontrada)
            throw new DomainException("Proposta não existe.");

        if (status.Situacao == PropostaSituacao.NaoAprovada)
            throw new DomainException("Proposta não está aprovada.");


        var jaContratada = await _contratacaoRepository
            .ExisteContratacaoParaPropostaAsync(command.PropostaId);

        if (jaContratada)
        {
            _logger.LogWarning(
                "Tentativa de contratação duplicada da proposta {PropostaId}",
                command.PropostaId);

            throw new DomainException("Proposta já foi contratada");
        }

        var contratacao = new Domain.Entities.Contratacao(command.PropostaId);

        await _contratacaoRepository.AddAsync(contratacao);

        _logger.LogInformation(
           "Contratação criada para proposta {PropostaId}",
           command.PropostaId);

        return new ContratarPropostaResult(
            contratacao.Id,
            contratacao.PropostaId,
            contratacao.DataContratacao
        );
    }
}
