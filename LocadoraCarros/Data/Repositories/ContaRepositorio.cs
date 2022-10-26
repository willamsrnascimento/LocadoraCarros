using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Repositories
{
    public class ContaRepositorio : RepositorioGenerico<Conta>, IContaRepositorio
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ContaRepositorio(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public int PegarSaldoPeloId(string id)
        {
            return _applicationDbContext.Contas.FirstOrDefault(c => c.UsuarioId == id).Saldo;
        }

        public new async Task<IEnumerable<Conta>> PegarTodos()
        {
            return await _applicationDbContext.Contas.Include(c => c.Usuario).ToListAsync();
        }

        public async Task<Conta> PegarSaldoPeloUsuarioId(string id)
        {
            return await _applicationDbContext.Contas.FirstOrDefaultAsync(c => c.UsuarioId == id);
        }
    }
}
