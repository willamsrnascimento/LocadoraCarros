using LocadoraCarros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraCarros.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Rua).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Numero).IsRequired();
            builder.Property(e => e.Bairro).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Estado).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.Usuario).WithMany(u => u.Enderecos).HasForeignKey(e => e.UsuarioId);

            builder.ToTable("Enderecos");
        }
    }
}
