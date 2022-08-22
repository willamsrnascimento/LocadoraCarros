namespace LocadoraCarros.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int Saldo { get; set; }
    }
}
