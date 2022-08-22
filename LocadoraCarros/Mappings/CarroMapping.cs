using LocadoraCarros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraCarros.Mappings
{
    public class CarroMapping : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Marca).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Foto).IsRequired();
            builder.Property(c => c.PrecoDiaria).IsRequired();

            builder.HasMany(c => c.Alugueis).WithOne(c => c.Carro).OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Carros");
        }
    }
}
