namespace Seguro.Hexagonal.Proposta.Domain.Interfaces.Usecases;
public interface IListarPropostasUseCase
{
    Task<IReadOnlyList<Entities.Proposta>> ExecuteAsync();
}

