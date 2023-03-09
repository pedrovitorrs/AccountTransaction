using AccountTransaction.WebUI.Services.Interface;
using AccountTransaction.WebUI.ViewModel.Base;
using AccountTransaction.WebUI.ViewModel.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AccountTransaction.WebUI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly IAccountService _accountService;

        public HomeController(
            ILogger<HomeController> logger, 
            ITransactionService transactionService,
            IAccountService accountService)
        {
            _logger = logger;
            _transactionService = transactionService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index([FromQuery] int ps = 100, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var accounts = await _accountService.ListAll(ps, page, q);
            var transactions = await _transactionService.ListAll();
            var totalTransacao = transactions.Sum(transacao => transacao.Valor_Transacao);

            var indexViewModel = new IndexViewModel()
            {
                Total_Transacoes = transactions.Count,
                Volume_Transacionado = totalTransacao,
                Ticket_Medio = totalTransacao/ transactions.Count,
                Cartoes_Cadastrados = accounts?.List.Sum(conta => conta.Cartoes.Count)
            };
            return View(indexViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        [Route("home/transactions")]
        public async Task<IActionResult> Transactions()
        {
            var Response = await _transactionService.ListAll();
            return Json(Response);
        }
    }
}