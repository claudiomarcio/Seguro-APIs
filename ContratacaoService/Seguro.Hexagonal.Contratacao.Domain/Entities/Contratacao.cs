using Seguro.Hexagonal.Contratacao.Domain.Exceptions;

namespace Seguro.Hexagonal.Contratacao.Domain.Entities;

public class Contratacao
{
    public Guid Id { get; private set; }
    public Guid PropostaId { get; private set; }
    public DateTime DataContratacao { get; private set; }

    protected Contratacao() { } // ORM-safe

    public Contratacao(Guid propostaId)
    {
        if (propostaId == Guid.Empty)
            throw new DomainException("PropostaId é obrigatório.");

        Id = Guid.NewGuid();
        PropostaId = propostaId;
        DataContratacao = DateTime.UtcNow;
    }
}
