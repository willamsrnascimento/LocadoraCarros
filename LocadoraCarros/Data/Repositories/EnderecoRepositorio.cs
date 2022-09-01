using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Repositories
{
    public class EnderecoRepositorio : RepositorioGenerico<Endereco>, IEnderecoRepositorio
    {
        public EnderecoRepositorio(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
