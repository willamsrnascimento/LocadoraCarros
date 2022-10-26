using LocadoraCarros.Models;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Interfaces
{
    public interface IAluguelRepositorio : IRepositorioGenerico<Aluguel>
    {
        Task<bool> VerificarReserva(string usuarioId, int carroId, string dataInicio, string dataFim);
    }
}
