using System.ComponentModel.DataAnnotations;

namespace LocadoraCarros.ViewModels
{
    public class AluguelViewModel
    {
        public int CarroId { get; set; }
        public int PrecoDiaria { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string DataInicio { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório.")]
        public string DataFim { get; set; }
        public int PrecoTotal { get; set; }

    }
}
