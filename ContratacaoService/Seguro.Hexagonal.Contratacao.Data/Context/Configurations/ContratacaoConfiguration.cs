using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Seguro.Hexagonal.Contratacao.Data.Context.Configurations;

public class ContratacaoConfiguration : IEntityTypeConfiguration<Domain.Entities.Contratacao>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Contratacao> builder)
    {
        builder.ToTable("Contratacoes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.PropostaId)
            .IsRequired();

        builder.Property(c => c.DataContratacao)
            .IsRequired();

        builder.HasIndex(c => c.PropostaId)
            .IsUnique();
    }
}
