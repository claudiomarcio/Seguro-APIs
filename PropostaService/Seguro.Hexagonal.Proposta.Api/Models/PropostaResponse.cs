namespace Seguro.Hexagonal.Proposta.Api.Models;
public class PropostaResponse
{
    public Guid Id { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}

