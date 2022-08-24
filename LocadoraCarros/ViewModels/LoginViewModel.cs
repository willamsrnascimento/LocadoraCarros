using System.ComponentModel.DataAnnotations;

namespace LocadoraCarros.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
