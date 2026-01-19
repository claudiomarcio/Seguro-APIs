namespace Seguro.Hexagonal.Contratacao.Domain.Commands;

public class ContratarPropostaCommand
{
    public Guid PropostaId { get; }

    public ContratarPropostaCommand(Guid propostaId)
    {
        PropostaId = propostaId;
    }
}