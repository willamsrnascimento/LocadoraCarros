using LocadoraCarros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraCarros.Mappings
{
    public class NiveisAcessoMapping : IEntityTypeConfiguration<NiveisAcesso>
    {
        public void Configure(EntityTypeBuilder<NiveisAcesso> builder)
        {
            builder.Property(na => na.Descricao).IsRequired().HasMaxLength(400);

            builder.ToTable("NiveisAcesso");
        }
    }
}
