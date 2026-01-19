using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropostaEntities = Seguro.Hexagonal.Proposta.Domain.Entities.Proposta;
namespace Seguro.Hexagonal.Data.Context.Configurations;

public class PropostaConfiguration : IEntityTypeConfiguration<PropostaEntities>
{
    public void Configure(EntityTypeBuilder<PropostaEntities> builder)
    {
        builder.ToTable("Propostas");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Cliente)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.DataCriacao)
            .IsRequired();

        builder.HasIndex(p => p.Status);
    }
}
