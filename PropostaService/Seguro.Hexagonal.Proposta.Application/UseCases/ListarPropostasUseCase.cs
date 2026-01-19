using Seguro.Hexagonal.Proposta.Domain.Interfaces.Repositories;
using Seguro.Hexagonal.Proposta.Domain.Interfaces.Usecases;
using PropostaEntities = Seguro.Hexagonal.Proposta.Domain.Entities.Proposta;
namespace Seguro.Hexagonal.Application.UseCases;

public class ListarPropostasUseCase : IListarPropostasUseCase
{
    private readonly IPropostaRepository _repository;

    public ListarPropostasUseCase(IPropostaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<PropostaEntities>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
