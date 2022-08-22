namespace LocadoraCarros.Models
{
    public class Aluguel
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int CarroId { get; set; }
        public Carro Carro { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public int PrecoTotal { get; set; }

    }
}