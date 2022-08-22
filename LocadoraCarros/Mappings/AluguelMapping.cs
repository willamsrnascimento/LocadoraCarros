using LocadoraCarros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraCarros.Mappings
{
    public class AluguelMapping : IEntityTypeConfiguration<Aluguel>
    {
        public void Configure(EntityTypeBuilder<Aluguel> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DataInicio).IsRequired();
            builder.Property(a => a.DataFim).IsRequired();
            builder.Property(a => a.PrecoTotal).IsRequired();

            builder.HasOne(a => a.Usuario).WithMany(u => u.Alugueis).HasForeignKey(a => a.UsuarioId);
            builder.HasOne(a => a.Carro).WithMany(u => u.Alugueis).HasForeignKey(a => a.CarroId);

            builder.ToTable("Alugueis");
        }
    }
}
