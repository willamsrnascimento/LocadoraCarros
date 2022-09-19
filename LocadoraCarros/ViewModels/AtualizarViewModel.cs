using System.ComponentModel.DataAnnotations;

namespace LocadoraCarros.ViewModels
{
    public class AtualizarViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "Use menos caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "Use menos caracteres.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "Use menos caracteres.")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [StringLength(100, ErrorMessage = "Use menos caracteres.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

    }
}
