using Seguro.Hexagonal.Application.Commands;
using Seguro.Hexagonal.Proposta.Application.Interfaces;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;
using PropostaEntities = Seguro.Hexagonal.Proposta.Domain.Entities.Proposta;

namespace Seguro.Hexagonal.Application.UseCases;

public class CriarPropostaUseCase : ICriarPropostaUseCase
{
    private readonly IPropostaRepository _repository;

    public CriarPropostaUseCase(IPropostaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> ExecuteAsync(CriarPropostaCommand command)
    {
        var proposta = new PropostaEntities(command.Cliente);

        await _repository.AddAsync(proposta);

        return proposta.Id;
    }
}
