using LocadoraCarros.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Interfaces
{
    public interface IContaRepositorio : IRepositorioGenerico<Conta>
    {
        new Task<IEnumerable<Conta>> PegarTodos();
        int PegarSaldoPeloId(string id);
    }
}
