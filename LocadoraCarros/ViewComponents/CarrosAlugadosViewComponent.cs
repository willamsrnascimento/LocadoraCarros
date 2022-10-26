using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraCarros.ViewComponents
{
    public class CarrosAlugadosViewComponent : ViewComponent
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ApplicationDbContext _applicationDbContext;

        public CarrosAlugadosViewComponent(IUsuarioRepositorio usuarioRepositorio, ApplicationDbContext applicationDbContext)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuarioLogado = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);

            return View(await _applicationDbContext.Alugueis.Include(a => a.Carro).Where(a => a.UsuarioId == usuarioLogado.Id).ToArrayAsync());
        }
    }
}
