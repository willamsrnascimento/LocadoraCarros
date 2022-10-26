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
using Microsoft.AspNetCore.Authorization;

namespace LocadoraCarros.Controllers
{
    [Authorize]
    public class EnderecosController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;
        private readonly ILogger<EnderecosController> _logger;

        public EnderecosController(IUsuarioRepositorio usuarioRepositorio, IEnderecoRepositorio enderecoRepositorio, ILogger<EnderecosController> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _enderecoRepositorio = enderecoRepositorio;
            _logger = logger;
        }

        // GET: Enderecos/Create
        public async Task<IActionResult> Create()
        {
            _logger.LogInformation("Novo Endereço");
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(User);
            var endereco = new Endereco { UsuarioId = usuario.Id };
            return View(endereco);
        }

        // POST: Enderecos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rua,Numero,Bairro,Cidade,Estado,UsuarioId")] Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await _enderecoRepositorio.Inserir(endereco);
                _logger.LogInformation("Novo endereço cadastrado.");
                return RedirectToAction("Index", "Usuarios");
            }

            _logger.LogError("Erro no cadastro de endereço.");
            return View(endereco);
        }

        // GET: Enderecos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Atualizando endereços.");

            var endereco = await _enderecoRepositorio.PegarPeloId(id);

            if (endereco == null)
            {
                _logger.LogError("Endereço não encontrado.");
                return NotFound();
            }

            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rua,Numero,Bairro,Cidade,Estado,UsuarioId")] Endereco endereco)
        {
            if (id != endereco.Id)
            {
                _logger.LogError("Endereço não encontrado.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _enderecoRepositorio.Atualizar(endereco);
                _logger.LogInformation("Endereço atualizado.");
                return RedirectToAction("Index", "Usuarios");
            }

            _logger.LogError("Endereço inválido.");
            return View(endereco);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            await _enderecoRepositorio.Excluir(id);
            _logger.LogInformation("Endereço excluído.");
            return Json("Endereço excluído."); 
        }
    }
}
