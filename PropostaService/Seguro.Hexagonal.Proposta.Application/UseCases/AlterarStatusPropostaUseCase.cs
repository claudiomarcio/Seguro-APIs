using Seguro.Hexagonal.Domain.Commands;
using Seguro.Hexagonal.Domain.Enums;
using Seguro.Hexagonal.Domain.Exceptions;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Usecases;

namespace Seguro.Hexagonal.Application.UseCases;

public class AlterarStatusPropostaUseCase : IAlterarStatusPropostaUseCase
{
    private readonly IPropostaRepository _repository;

    public AlterarStatusPropostaUseCase(IPropostaRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AlterarStatusPropostaCommand command)
    {
        var proposta = await _repository.GetByIdAsync(command.PropostaId);

        if (proposta is null)
            throw new DomainException("Proposta não encontrada.");

        switch (command.NovoStatus)
        {
            case StatusProposta.Aprovada:
                proposta.Aprovar();
                break;

            case StatusProposta.Rejeitada:
                proposta.Rejeitar();
                break;

            default:
                throw new DomainException("Status inválido para alteração.");
        }

        await _repository.UpdateAsync(proposta);
    }
}
