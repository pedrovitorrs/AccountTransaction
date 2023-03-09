using AccountTransaction.WebUI.Services.Interface;
using AccountTransaction.WebUI.ViewModel.Base;
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

        public HomeController(ILogger<HomeController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        public IActionResult Index()
        {
            return View();
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