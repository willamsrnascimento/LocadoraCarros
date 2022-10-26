using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Repositories
{
    public class AluguelRepositorio : RepositorioGenerico<Aluguel>, IAluguelRepositorio
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AluguelRepositorio(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> VerificarReserva(string usuarioId, int carroId, string dataInicio, string dataFim)
        {
            return await _applicationDbContext.Alugueis.AnyAsync(a => a.UsuarioId == usuarioId && a.CarroId == carroId && a.DataInicio == dataInicio && a.DataFim == dataFim);
        }
    }
}
