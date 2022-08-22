using System.Collections.Generic;

namespace LocadoraCarros.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Foto { get; set; }
        public int PrecoDiaria { get; set; }
        public ICollection<Aluguel> Alugueis { get; set; }
    }
}
