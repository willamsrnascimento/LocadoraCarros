using LocadoraCarros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraCarros.Mappings
{
    public class ContaMapping : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Saldo).IsRequired();

            builder.HasOne(c => c.Usuario).WithOne(u => u.Conta).HasForeignKey<Conta>(c => c.UsuarioId);

            builder.ToTable("Contas");
        }
    }
}
