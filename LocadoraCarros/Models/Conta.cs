using System.ComponentModel.DataAnnotations;

namespace LocadoraCarros.Models
{
    public class Conta
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "Valor inválido.")]
        public int Saldo { get; set; }
    }
}
