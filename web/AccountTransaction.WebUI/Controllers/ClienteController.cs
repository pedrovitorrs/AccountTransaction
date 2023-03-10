using AccountTransaction.WebUI.Services.Interface;
using AccountTransaction.WebUI.ViewModel.Base;
using AccountTransaction.WebUI.ViewModel.Cartao;
using AccountTransaction.WebUI.ViewModel.Cliente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;

namespace AccountTransaction.WebUI.Controllers
{
    [Authorize]
    public class ClienteController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;

        public ClienteController(ILogger<HomeController> logger, IAccountService accountService)
        {
            _accountService = accountService;
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromQuery] int ps = 4, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            return View(await _accountService.ListAll(ps, page, q));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
