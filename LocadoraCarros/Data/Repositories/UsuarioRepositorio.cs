using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LocadoraCarros.Data.Repositories
{
    public class UsuarioRepositorio : RepositorioGenerico<Usuario>, IUsuarioRepositorio
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;

        public UsuarioRepositorio(ApplicationDbContext applicationDbContext,SignInManager<Usuario> signInManager, UserManager<Usuario> userManager) : base(applicationDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task AtribuirNivelAcesso(Usuario usuario, string nivelAcesso)
        {
            await _userManager.AddToRoleAsync(usuario, nivelAcesso);
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            await _userManager.UpdateAsync(usuario);
        }

        public async Task EfetuarLogin(Usuario usuario, bool lembrar)
        {
            await _signInManager.SignInAsync(usuario, lembrar); 
        }

        public async Task EfetuarLogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Usuario> PegarUsuarioLogado(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }

        public async Task<Usuario> PegarUsuarioPeloEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> SalvarUsuario(Usuario usuario, string senha)
        {
            return await _userManager.CreateAsync(usuario, senha);
        }
    }
}
