using System.ComponentModel.DataAnnotations;

namespace LocadoraCarros.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos caractéres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos caractéres")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos caractéres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, ErrorMessage = "Use menos caractéres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

    }
}
