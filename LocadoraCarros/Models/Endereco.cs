using System.ComponentModel.DataAnnotations;

namespace LocadoraCarros.Models
{
    public class Endereco
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "Use menos caracteres")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Range(0, int.MaxValue, ErrorMessage = "Valor inválido.")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(20, ErrorMessage = "Use menos caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(20, ErrorMessage = "Use menos caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(20, ErrorMessage = "Use menos caracteres")]
        public string Estado { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}