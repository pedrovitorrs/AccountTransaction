using Microsoft.AspNetCore.Mvc;

namespace AccountTransaction.Account.API.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
