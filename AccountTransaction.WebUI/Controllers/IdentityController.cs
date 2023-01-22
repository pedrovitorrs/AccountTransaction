using AccountTransaction.WebUI.Models;
using AccountTransaction.WebUI.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransaction.WebUI.Controllers
{
    [Route("users")]
    public class IdentityController : BaseController
    {
        private readonly IAuthService _authService;

        public IdentityController(
            IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl)
        {
            var remember = GetCookie("RememberMe");
            var loginViewModel = new UserLoginViewModel()
            {
                RememberMe = "True".Equals(remember),
                ReturnUrl = returnUrl
            };

            return View(loginViewModel);
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
