using LocadoraCarros.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocadoraCarros.Models
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, NiveisAcesso, string>
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NiveisAcesso> NiveisAcessos { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Aluguel> Alugueis { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UsuarioMapping());
            builder.ApplyConfiguration(new NiveisAcessoMapping());
            builder.ApplyConfiguration(new ContaMapping());
            builder.ApplyConfiguration(new EnderecoMapping());
            builder.ApplyConfiguration(new CarroMapping());
            builder.ApplyConfiguration(new AluguelMapping());
        }
    }
}
