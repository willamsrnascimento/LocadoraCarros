using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;

namespace LocadoraCarros.Data.Repositories
{
    public class CarroRepositorio : RepositorioGenerico<Carro>, ICarroRepositorio
    {
        public CarroRepositorio(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
