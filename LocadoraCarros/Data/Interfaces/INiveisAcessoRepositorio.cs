using LocadoraCarros.Models;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Interfaces
{
    public interface INiveisAcessoRepositorio : IRepositorioGenerico<NiveisAcesso>
    {
        Task<bool> NiveisAcessoExiste(string nivelAcesso);
    }
}
