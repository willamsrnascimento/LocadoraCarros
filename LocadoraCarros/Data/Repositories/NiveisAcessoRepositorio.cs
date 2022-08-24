using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Repositories
{
    public class NiveisAcessoRepositorio : RepositorioGenerico<NiveisAcesso>, INiveisAcessoRepositorio
    {
        private readonly RoleManager<NiveisAcesso> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public NiveisAcessoRepositorio(ApplicationDbContext applicationDbContext, RoleManager<NiveisAcesso> roleManager) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _roleManager = roleManager;
        }

        public async Task<bool> NiveisAcessoExiste(string nivelAcesso)
        {
            return await _roleManager.RoleExistsAsync(nivelAcesso);
        }
    }
}
