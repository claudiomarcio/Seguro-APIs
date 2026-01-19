using Seguro.Hexagonal.Contratacao.Domain.Enums;

namespace Seguro.Hexagonal.Contratacao.Domain.ValueObjects;

public record PropostaStatusResult(
    Guid PropostaId,
    PropostaSituacao Situacao
);
