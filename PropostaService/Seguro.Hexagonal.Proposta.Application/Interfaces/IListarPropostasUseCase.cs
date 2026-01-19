namespace Seguro.Hexagonal.Proposta.Application.Interfaces;
public interface IListarPropostasUseCase
{
    Task<IReadOnlyList<Domain.Entities.Proposta>> ExecuteAsync();
}

