namespace Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

public record ContratarPropostaResult(
    Guid Id,
    Guid PropostaId,
    DateTime DataContratacao
);
