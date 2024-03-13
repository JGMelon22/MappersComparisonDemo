using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MappersWebApiDemo.Infrastructure.Configuration;

public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produtos");

        builder.HasKey(p => p.Id)
            .HasName("pk_produto");

        builder.HasIndex(p => p.Id)
            .HasDatabaseName("idx_produto_id");

        builder.Property(p => p.Id)
            .HasColumnType("INTEGER")
            .HasColumnName("produto_id")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nome)
            .HasColumnType("TEXT")
            .HasColumnName("nome")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Preco)
            .HasColumnType("REAL")
            .HasColumnName("preco")
            .IsRequired();

        builder.Property(p => p.Disponivel)
            .HasColumnType("BOOLEAN") // Apenas um alias interno no SQLite, pois ele trabalha com INTEGER nesse caso
            .HasColumnName("disponivel")
            .IsRequired();
    }
}