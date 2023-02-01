using AccountTransaction.Commom.Core.UserIdentity;
using AccountTransaction.WebUI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AccountTransaction.WebUI.ViewComponents
{
    [ViewComponent(Name = "_LayoutNavBarViewComponent")]
    public class LayoutNavBarViewComponent : ViewComponent
    {
        private readonly IAspNetUser _userManager;

        public LayoutNavBarViewComponent(IAspNetUser userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var appUser = _userManager;

            var vm = new LayoutNavBarViewModel
            {
                UserName = appUser?.Name,
                Email = appUser?.GetUserEmail()
            };

            return View("../Shared/_LayoutNavBar.cshtml", vm);
        }
    }
}
