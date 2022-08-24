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
    public class NiveisAcessosController : Controller
    {
        private readonly INiveisAcessoRepositorio _niveisAcessoRepositorio;
        private readonly ILogger<NiveisAcessosController> _logger;

        public NiveisAcessosController(INiveisAcessoRepositorio niveisAcessoRepositorio, ILogger<NiveisAcessosController> logger)
        {
            _niveisAcessoRepositorio = niveisAcessoRepositorio;
            _logger = logger;
        }

        // GET: NiveisAcessos
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listanto todos os registros.");
            return View(await _niveisAcessoRepositorio.PegarTodos().ToListAsync());
        }

        // GET: NiveisAcessos/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando criação de Níveis de Acesso.");
            return View();
        }

        // POST: NiveisAcessos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Name")] NiveisAcesso niveisAcesso)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Verificando se nível de acesso existe.");
                bool nivelExiste = await _niveisAcessoRepositorio.NiveisAcessoExiste(niveisAcesso.Name);

                if (!nivelExiste)
                {
                    niveisAcesso.NormalizedName = niveisAcesso.Name.ToUpper();
                    await _niveisAcessoRepositorio.Inserir(niveisAcesso);
                    _logger.LogInformation("Novo nível de acesso criado.");

                    return RedirectToAction(nameof(Index));
                }

            }

            _logger.LogError("Informações inválidas.");
            return View(niveisAcesso);
        }

        // GET: NiveisAcessos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            _logger.LogInformation("Atualizando nível de acesso");

            if (id == null)
            {
                _logger.LogInformation("Nível de acesso não encontrado");
                return NotFound();
            }

            var niveisAcesso = await _niveisAcessoRepositorio.PegarPeloId(id);  
            if (niveisAcesso == null)
            {
                return NotFound();
            }

            return View(niveisAcesso);
        }

        // POST: NiveisAcessos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Descricao,Id,Name,NormalizedName,ConcurrencyStamp")] NiveisAcesso niveisAcesso)
        {
            if (id != niveisAcesso.Id)
            {
                _logger.LogInformation("Nível de acesso não encontrado");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _niveisAcessoRepositorio.Atualizar(niveisAcesso);
                _logger.LogInformation("Nível de acesso atualizado.");

                return RedirectToAction(nameof(Index));
            }

            _logger.LogError("Informações inválidas.");
            return View(niveisAcesso);
        }

        // POST: NiveisAcessos/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _niveisAcessoRepositorio.Excluir(id);
            _logger.LogInformation("Nível de acesso exclído.");
            return RedirectToAction(nameof(Index));
        }

    }
}
