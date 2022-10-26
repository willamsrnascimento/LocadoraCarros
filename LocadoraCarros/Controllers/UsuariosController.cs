﻿using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using LocadoraCarros.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LocadoraCarros.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuariosController> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando informações");

            return View(await _usuarioRepositorio.PegarUsuarioLogado(User));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Registro()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _usuarioRepositorio.EfetuarLogOut();
            }

            _logger.LogInformation("Entrando na página de registro.");

            return View();
        }

        [AllowAnonymous]
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

                    return RedirectToAction("Index", "Usuarios");
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

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _usuarioRepositorio.EfetuarLogOut();
            }

            _logger.LogInformation("Entrando na página de login.");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Pegado usuário pelo email.");

                var usuario = await _usuarioRepositorio.PegarUsuarioPeloEmail(loginViewModel.Email);

                PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

                if(usuario != null)
                {
                    _logger.LogInformation("Verificando informações do usuário");

                    if(passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, loginViewModel.Senha) != PasswordVerificationResult.Failed)
                    {
                        _logger.LogInformation("Informações corretas. Logando o usuário.");

                        await _usuarioRepositorio.EfetuarLogin(usuario, false);

                        return RedirectToAction("Index", "Usuarios");
                    }

                    _logger.LogError("Informações inválidas.");
                    ModelState.AddModelError("", "E-mail e/ou senha inválidos");
                }

                _logger.LogError("Informações inválidas.");
                ModelState.AddModelError("", "E-mail e/ou senha inválidos");
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> LogOut()
        {
            await _usuarioRepositorio.EfetuarLogOut();

            return RedirectToAction("Login", "Usuarios");   
        }

        [HttpGet]
        public async Task<IActionResult> Atualizar(string Id)
        {
            _logger.LogInformation("Verificando se o usuário existe.");

            var usuario = await _usuarioRepositorio.PegarPeloId(Id);

            var atualizarViewModel = new AtualizarViewModel()
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                CPF = usuario.CPF,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                NomeUsuario = usuario.UserName
            };

            return View(atualizarViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizar(AtualizarViewModel atualizarViewModel)
        {
            _logger.LogInformation("Atualizar usuário.");

            if (ModelState.IsValid)
            {
                var usuario = await _usuarioRepositorio.PegarPeloId(atualizarViewModel.Id);

                usuario.Nome = atualizarViewModel.Nome;
                usuario.CPF = atualizarViewModel.CPF;
                usuario.UserName = atualizarViewModel.NomeUsuario;
                usuario.Email = atualizarViewModel.Email;
                usuario.Telefone = atualizarViewModel.Telefone;

                await _usuarioRepositorio.Atualizar(usuario);

                _logger.LogInformation("Usuário atualizado.");

                return RedirectToAction("Index", "Usuarios");
            }

            _logger.LogError("Informações inválidas.");

            return View(atualizarViewModel);
        }
    }
}
