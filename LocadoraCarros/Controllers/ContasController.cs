using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocadoraCarros.Models;
using LocadoraCarros.Data.Interfaces;
using Microsoft.Extensions.Logging;

namespace LocadoraCarros.Controllers
{
    public class ContasController : Controller
    {
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<ContasController> _logger;

        public ContasController(IContaRepositorio contaRepositorio, IUsuarioRepositorio usuarioRepositorio, ILogger<ContasController> logger)
        {
            _contaRepositorio = contaRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }



        // GET: Contas
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando os saldos.");
            return View(await _contaRepositorio.PegarTodos());
        }


        // GET: Contas/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Criar novo saldo");
            ViewData["UsuarioId"] = new SelectList(_usuarioRepositorio.PegarTodos(), "Id", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Saldo")] Conta conta)
        {
            if (ModelState.IsValid)
            {
                await _contaRepositorio.Inserir(conta);
                _logger.LogInformation("Novo saldo criado.");
                return RedirectToAction(nameof(Index));
            }

            _logger.LogError("Informações inválidas");
            ViewData["UsuarioId"] = new SelectList(_usuarioRepositorio.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Atualizar conta");

            var conta = await _contaRepositorio.PegarPeloId(id);
            if (conta == null)
            {
                _logger.LogError("Conta não localizada.");
                return NotFound();
            }

            _logger.LogError("Informações inválidas");
            ViewData["UsuarioId"] = new SelectList(_usuarioRepositorio.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,Saldo")] Conta conta)
        {
            if (id != conta.Id)
            {
                _logger.LogError("Conta não encontrada.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _contaRepositorio.Atualizar(conta);
                return RedirectToAction(nameof(Index));
            }

            _logger.LogError("Informações inválidas.");
            ViewData["UsuarioId"] = new SelectList(await _contaRepositorio.PegarTodos(), "Id", "Email", conta.UsuarioId);
            return View(conta);
        }

    }
}
