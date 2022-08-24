using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using LocadoraCarros.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LocadoraCarros.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuariosController> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Registro()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _usuarioRepositorio.EfetuarLogOut();
            }

            _logger.LogInformation("Entrando na página de registro.");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel registroViewModel)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario
                {
                    UserName = registroViewModel.NomeUsuario,
                    Email = registroViewModel.Email,
                    CPF = registroViewModel.CPF,
                    Telefone = registroViewModel.Telefone,
                    Nome = registroViewModel.Nome,
                    PasswordHash = registroViewModel.Senha
                };

                _logger.LogInformation("Tentando criar um novo usuário.");

                var resultado = await _usuarioRepositorio.SalvarUsuario(usuario, registroViewModel.Senha);

                if (resultado.Succeeded)
                {
                    _logger.LogInformation("Novo usuário criado com sucesso.");
                    _logger.LogInformation("Atribuindo nível de acesso ao usuário.");

                    var nivelAcesso = "Cliente";
                    await _usuarioRepositorio.AtribuirNivelAcesso(usuario, nivelAcesso);

                    _logger.LogInformation("Atribuição concluída.");
                    _logger.LogInformation("Logando usuário");

                    await _usuarioRepositorio.EfetuarLogin(usuario, false);

                    _logger.LogInformation("Usuário logado com sucesso.");

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogError("Erro ao criar usuário.");

                    foreach(var erro in resultado.Errors)
                    {
                        ModelState.AddModelError("", erro.Description.ToString());
                    }
                }
            }

            _logger.LogError("Informações de usuário inválidas.");

            return View(registroViewModel);
        }

    }
}
