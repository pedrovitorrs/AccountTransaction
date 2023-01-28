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


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginViewModel userLogin, string returnUrl = null)
        {
            if (!ModelState.IsValid) return View(userLogin);

            var resposta = await _authService.Login(userLogin);

            if (ResponseHasErrors(resposta.ResponseResult)) 
                return View(userLogin);

            await _authService.DoLogin(resposta);

            if (string.IsNullOrEmpty(returnUrl)) 
                return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
