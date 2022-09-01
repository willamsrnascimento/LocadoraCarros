using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LocadoraCarros.ViewComponents
{
    public class EnderecoViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _applicationDBContext;
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public EnderecoViewComponent(ApplicationDbContext applicationDBContext, IUsuarioRepositorio usuarioRepositorio)
        {
            _applicationDBContext = applicationDBContext;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);

            return View(await _applicationDBContext.Enderecos.Where(e => e.UsuarioId == usuario.Id).ToListAsync());
        }
    }
}
