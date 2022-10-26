using LocadoraCarros.Data.Interfaces;
using LocadoraCarros.Models;
using LocadoraCarros.Services;
using LocadoraCarros.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LocadoraCarros.Controllers
{
    public class AlugueisController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContaRepositorio _contaRepositorio;
        private readonly IAluguelRepositorio _aluguelRepositorio;
        private readonly ILogger<AlugueisController> _logger;
        private readonly IEmail _email;

        public AlugueisController(IUsuarioRepositorio usuarioRepositorio, IContaRepositorio contaRepositorio, IAluguelRepositorio aluguelRepositorio, ILogger<AlugueisController> logger, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _contaRepositorio = contaRepositorio;
            _aluguelRepositorio = aluguelRepositorio;
            _logger = logger;
            _email = email;
        }

        public IActionResult Alugar(int carroId, int precoDiaria)
        {
            _logger.LogInformation("Começando o aluguel do carro.");

            var aluguel = new AluguelViewModel
            {
                CarroId = carroId,
                PrecoDiaria = precoDiaria
            };

            return View(aluguel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Alugar(AluguelViewModel aluguelViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _usuarioRepositorio.PegarUsuarioLogado(User);
                var saldo = _contaRepositorio.PegarSaldoPeloId(usuario.Id);

                if (await _aluguelRepositorio.VerificarReserva(usuario.Id, aluguelViewModel.CarroId, aluguelViewModel.DataInicio, aluguelViewModel.DataFim))
                {
                    _logger.LogInformation("Usuário já possui essa reserva.");
                    TempData["Aviso"] = "Você já possui essa reserva.";
                    return View(aluguelViewModel);
                }
                else if(aluguelViewModel.PrecoTotal > saldo)
                {
                    _logger.LogInformation("Saldo insuficiente.");
                    TempData["Aviso"] = "Saldo insuficiente.";
                    return View(aluguelViewModel);
                }
                else
                {
                    Aluguel a = new Aluguel
                    {
                        UsuarioId = usuario.Id,
                        CarroId = aluguelViewModel.CarroId,
                        DataInicio = aluguelViewModel.DataInicio,
                        DataFim = aluguelViewModel.DataFim,
                        PrecoTotal = aluguelViewModel.PrecoTotal
                    };

                    _logger.LogInformation("Enviando email com detalhes da reserva.");
                    string assunto = "Reserva concluída com sucesso.";
                    string mensagem = string.Format($"Seu veículo já o aguarda. Você poderá pega-lo {aluguelViewModel.DataInicio}" +
                                                    $"e deverá devolve-lo {aluguelViewModel.DataFim}. O preço será R${aluguelViewModel.PrecoTotal},00. Divirta-se!!!");

                    await _email.EnviarEmail(usuario.Email, assunto, mensagem);

                    await _aluguelRepositorio.Inserir(a);
                    _logger.LogInformation("Reserva feita.");

                    _logger.LogInformation("Atualizando saldo do usuário.");
                    var saldoUsuario = await _contaRepositorio.PegarSaldoPeloUsuarioId(usuario.Id);
                    saldoUsuario.Saldo = saldoUsuario.Saldo - aluguelViewModel.PrecoTotal;
                    await _contaRepositorio.Atualizar(saldoUsuario);
                    _logger.LogInformation("Saldo atualizado.");

                    return RedirectToAction("Index", "Carros");
                }

            }

            _logger.LogError("Informações inválidas.");

            return View(aluguelViewModel);
        }
    }
}
