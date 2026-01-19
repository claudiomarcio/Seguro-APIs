namespace Seguro.Hexagonal.Contratacao.Application.Commands;

public class ContratarPropostaCommand
{
    public Guid PropostaId { get; }

    public ContratarPropostaCommand(Guid propostaId)
    {
        PropostaId = propostaId;
    }
}