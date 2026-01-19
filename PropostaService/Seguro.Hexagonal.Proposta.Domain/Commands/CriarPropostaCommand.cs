namespace Seguro.Hexagonal.Domain.Commands;

public class CriarPropostaCommand
{
    public string Cliente { get; }

    public CriarPropostaCommand(string cliente)
    {
        Cliente = cliente;
    }
}