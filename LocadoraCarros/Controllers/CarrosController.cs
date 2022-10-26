using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LocadoraCarros.Models;
using LocadoraCarros.Data.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace LocadoraCarros.Controllers
{
    [Authorize]
    public class CarrosController : Controller
    {
        private readonly ICarroRepositorio _carroRepositorio;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger<CarrosController> _logger;

        public CarrosController(ICarroRepositorio carroRepositorio, IHostingEnvironment hostingEnvironment, ILogger<CarrosController> logger)
        {
            _carroRepositorio = carroRepositorio;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando todos os carros.");
            return View(await _carroRepositorio.PegarTodos().ToListAsync());
        }

        public IActionResult Create()
        {
            _logger.LogInformation("Novo carro.");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    _logger.LogInformation("Criando link da pasta.");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Copiando arquivo para a pasta.");
                        await arquivo.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado.");
                        carro.Foto = "~/Imagens/" + arquivo.FileName;

                    }
                }

                _logger.LogInformation("Salvando novo carro.");
                await _carroRepositorio.Inserir(carro);
                return RedirectToAction(nameof(Index));
            }


            _logger.LogError("Informações inválidas.");
            return View(carro);
        }

        public async Task<IActionResult> Edit(int id)
        {
            _logger.LogInformation("Atualizar carro.");


            var carro = await _carroRepositorio.PegarPeloId(id);

            if (carro == null)
            {
                _logger.LogError("Carro não encontrado.");
                return NotFound();
            }

            TempData["FotoCarro"] = carro.Foto;

            return View(carro);
        }

        // POST: Carros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Marca,Foto,PrecoDiaria")] Carro carro, IFormFile arquivo)
        {
            if (id != carro.Id)
            {
                _logger.LogError("Carro não encontrado.");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    _logger.LogInformation("Criando link da pasta.");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");

                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("Copiando arquivo para a pasta.");
                        await arquivo.CopyToAsync(fileStream);
                        _logger.LogInformation("Arquivo copiado.");
                        carro.Foto = "~/Imagens/" + arquivo.FileName;

                    }
                }
                else
                {
                    carro.Foto = TempData["FotoCarro"].ToString();
                }

                _logger.LogInformation("Atualizar carro.");
                await _carroRepositorio.Atualizar(carro);
                return RedirectToAction(nameof(Index));
            }

            _logger.LogError("Dados inválidos.");
            return View(carro);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var carro = await _carroRepositorio.PegarPeloId(id);
            string fotoCarro = carro.Foto;

            fotoCarro = fotoCarro.Replace("~", "wwwroot");
            System.IO.File.Delete(fotoCarro);

            _logger.LogInformation("Excluindo carro.");
            await _carroRepositorio.Excluir(id);
            return Json("Carro excluído com sucesso.");
        }

    }
}
