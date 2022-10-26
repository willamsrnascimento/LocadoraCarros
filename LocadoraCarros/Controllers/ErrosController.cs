using Microsoft.AspNetCore.Mvc;

namespace LocadoraCarros.Controllers
{
    public class ErrosController : Controller
    {
        [HttpGet("Erros/{codigoErro}")]
        public IActionResult Index(int condigoErro)
        {
            return View(condigoErro);
        }
    }
}
