using AccountTransaction.WebUI.ViewModel.Base;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransaction.WebUI.ViewComponents
{
    [ViewComponent(Name = "paging")]
    public class PagingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList pagingModel)
        {
            return View(pagingModel);
        }
    }
}
