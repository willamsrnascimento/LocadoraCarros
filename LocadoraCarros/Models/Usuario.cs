using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LocadoraCarros.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Aluguel> Alugueis { get; set; }
        public Conta Conta { get; set; }

    }
}
